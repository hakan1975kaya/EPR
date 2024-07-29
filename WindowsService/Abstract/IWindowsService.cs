using WindowsService.Dtos.DoWorkDtos;
using WindowsService.Dtos.DoWorkStartDtos;
using WindowsService.Dtos.LoginDtos;

namespace WindowsService.Abstract
{
    public interface IWindowsService
    {
        Task<AccessTokenDataResult> Login(LoginRequestDto loginRequestDto);
        Task<DoWorkStartResponseDtoResult> DoWorkStart();
        Task<DoWorkResponseDtoResult> DoWork(string token);
        void OnStart();
        void OnStop();
        void OnPause();
        void OnContinue();


    }
}
