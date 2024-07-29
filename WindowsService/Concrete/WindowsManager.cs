using WindowsService.Abstract;
using WindowsService.Utilities.Constants;
using WindowsService.Dtos.LoginDtos;
using WindowsService.Dtos.DoWorkDtos;
using WindowsService.Dtos.DoWorkStartDtos;
using WindowsService.Dtos.WindowsServiceOption;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace WindowsService.Concrete
{
    public class WindowsManager : IWindowsService
    {
        private readonly IConfiguration _configuration;
        private WindowsServiceOption _windowsServiceOption;
        private bool isStopRequestReceived;
        private EventLog eventLog;
        public WindowsManager(IConfiguration configuration)
        {
            _configuration = configuration;

            _windowsServiceOption = _configuration.GetSection("WindowsServiceOption").Get<WindowsServiceOption>();

            if (!EventLog.SourceExists("WindowsService"))
            {
                EventLog.CreateEventSource("WindowsService", "WindowsService", Environment.MachineName);
            }
            eventLog = new EventLog();
            eventLog.Source = "WindowsService";
            eventLog.MachineName = Environment.MachineName;
            eventLog.Log = "WindowsService";

         
        }
        public async Task<AccessTokenDataResult> Login(LoginRequestDto loginRequestDto)
        {

            try
            {
                var client = new HttpClient();

                var body = JsonSerializer.Serialize<LoginRequestDto>(loginRequestDto);

                var data = new StringContent(body, Encoding.UTF8, "application/json");

                var url = _windowsServiceOption.BaseUrl + "Auths/login";

                var response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        eventLog.WriteEntry("Auths/login başarıyle çağrıldı. " + content.ToString());
                        return JsonSerializer.Deserialize<AccessTokenDataResult>(content);
                    }
                    else
                    {
                        eventLog.WriteEntry("Login Content null: " + response.ToString());
                    }

                }
                else
                {
                    eventLog.WriteEntry("Login Response Başarılı Değil: " + response.ToString());
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Login Hata Oluştu: " + ex.ToString());
            }
            return null;
        }

        public async Task<DoWorkResponseDtoResult> DoWork(string token)
        {
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var body = JsonSerializer.Serialize("");

                var data = new StringContent(body, Encoding.UTF8, "application/json");

                var url = _windowsServiceOption.BaseUrl + "Windows/doWork";

                var response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        eventLog.WriteEntry("Windows/doWork çağrıldı. " + content.ToString());
                        return JsonSerializer.Deserialize<DoWorkResponseDtoResult>(content);
                    }
                    else
                    {
                        eventLog.WriteEntry("DoWork Content null: " + response.ToString());
                    }

                }
                else
                {
                    eventLog.WriteEntry("DoWork Response Başarılı Değil: " + response.ToString());
                }

            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }
            return null;
        }
        public async Task<DoWorkStartResponseDtoResult> DoWorkStart()
        {
            try
            {
                var loginRequestDto = new LoginRequestDto
                {
                    RegistrationNumber = _windowsServiceOption.RegistrationNumber,
                    Password = _windowsServiceOption.Password
                };

                var accessTokenDataResult = await Login(loginRequestDto);

                if (accessTokenDataResult != null)
                {
                    if (accessTokenDataResult.Success)
                    {
                        if (accessTokenDataResult.AccessToken != null)
                        {
                            var accessToken = accessTokenDataResult.AccessToken;

                            if (accessToken != null)
                            {
                                if (accessToken.Token != null)
                                {
                                    var token = accessToken.Token;//created token

                                    var doWorkResponseDtoResult = await DoWork(token);

                                    if (doWorkResponseDtoResult != null)
                                    {
                                        if (doWorkResponseDtoResult.Success)
                                        {
                                            eventLog.WriteEntry("DoWorkStart Başarıyla Döndü");
                                            return new DoWorkStartResponseDtoResult { Success = true, Message = WindowsMessages.OperationSuccess };
                                        }
                                        else
                                        {
                                            eventLog.WriteEntry("DoWorkStart DoWork Success değil: " + doWorkResponseDtoResult.ToString());
                                        }
                                    }
                                    else
                                    {
                                        eventLog.WriteEntry("DoWorkStart DoWork null değil: " + doWorkResponseDtoResult.ToString());
                                    }
                                }
                                else
                                {
                                    eventLog.WriteEntry("DoWorkStart Token null " + accessToken.ToString());
                                }
                            }
                        }
                        else
                        {
                            eventLog.WriteEntry("DoWorkStart accessTokenDataResult.AccessToken null " + accessTokenDataResult.ToString());
                        }
                    }
                    else
                    {
                        eventLog.WriteEntry("DoWorkStart accessTokenDataResult success değil " + accessTokenDataResult.ToString());
                    }
                }
                else
                {
                    eventLog.WriteEntry("DoWorkStart accessTokenDataResult null ");
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }
            eventLog.WriteEntry("DoWorkStart null Döndü");
            return new DoWorkStartResponseDtoResult { Success = false, Message = WindowsMessages.OperationFaild };
        }

        public void OnStart()
        {
            try
            {
                eventLog.WriteEntry("Servis Çalımaya Başladı.");
                Task.Run(() => InitService());
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }

        }

        public void OnStop()
        {
            try
            {
                isStopRequestReceived = true;
                eventLog.WriteEntry("Servis Durdu.");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }

        }

        public void OnPause()
        {
            try
            {
                isStopRequestReceived = true;
                eventLog.WriteEntry("Servis Duraklatıldı");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }

        }

        public void OnContinue()
        {
            try
            {
                isStopRequestReceived = false;
                eventLog.WriteEntry("Servis Çalımaya Devam Ettirildi.");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }

        }

        private void InitService()
        {
            try
            {
                while (!isStopRequestReceived)
                {
                    eventLog.WriteEntry("Servis Çalışıyor");
                    DoWorkStart();
                    Thread.Sleep(3000000);
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Hata Oluştu: " + ex.ToString());
            }
        }


    }
}
