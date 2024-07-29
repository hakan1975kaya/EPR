
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Sftp.Dtos.SftpDownloadFileDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSaveDtos;
using Entities.Concrete.Enums.GeneralEnums;
using Core.Utilities.Results.Concrete;
using Business.Constants.Messages;
using Core.HttpAccessor.Abstract;
using DataAccess.Abstract;
using Core.Aspects.Autofac.Transaction;
using Entities.Concrete.Dtos.WorkerDtos;
using System.Diagnostics;
using System.Text;
using Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpStartMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpSuccessMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpErrorMailDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListPrefixAvailableDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpStopMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpPttMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpCorporateMailDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class WindowManager : IWindowService
    {
        private ICorporateService _corporateService;
        private ISftpService _sftpService;
        private ISftpDownloadService _sftpDownloadService;
        private IPaymentRequestService _paymentRequestService;
        private ITandemService _tandemService;
        private ISmtpService _smtpService;
        private ICorporateMailAddressService _corporateMailAddressService;
        private IHttpContextAccessorService _httpContextAccessorService;
        private IMailAddressService _mailAddressService;
        private IUserDal _userDal;
        private Stopwatch _stopwatch;
        public WindowManager(
            ICorporateService corporateService,
            ISftpService sftpService,
            ISftpDownloadService sftpDownloadService,
            IPaymentRequestService paymentRequestService,
            ITandemService tandemService,
            ISmtpService smtpService,
            ICorporateMailAddressService corporateMailAddressService,
            IHttpContextAccessorService httpContextAccessorService,
            IMailAddressService mailAddressService,
            IUserDal userDal
            )
        {
            _corporateService = corporateService;
            _sftpService = sftpService;
            _sftpDownloadService = sftpDownloadService;
            _paymentRequestService = paymentRequestService;
            _tandemService = tandemService;
            _smtpService = smtpService;
            _corporateMailAddressService = corporateMailAddressService;
            _httpContextAccessorService = httpContextAccessorService;
            _mailAddressService = mailAddressService;
            _userDal = userDal;
            _stopwatch = new Stopwatch();
        }

        [SecurityAspect("Window.DoWork", Priority = 2)]
        public async Task<IResult> DoWork()
        {
            _stopwatch.Start();

            var smtpStartMailResponseDto = new SmtpStartMailResponseDto
            {
                StartDate = DateTime.Now
            };

            var smtpSuccessMailResponseDtos = new List<SmtpSuccessMailResponseDto>();

            var smtpErrorMailResponseDtos = new List<SmtpErrorMailResponseDto>();

            var registrationNumberResult = await _httpContextAccessorService.GetRegistrationNumber();
            if (registrationNumberResult != null)
            {
                if (registrationNumberResult.Success)
                {
                    if (registrationNumberResult.Data != null)
                    {
                        var registrationNumber = registrationNumberResult.Data;
                        var user = await _userDal.Get(x => x.RegistrationNumber == registrationNumber && x.IsActive == true);
                        if (user != null)
                        {
                            var corporateGetListPrefixAvailableResponseDtoDataResult = await _corporateService.GetListPrefixAvailable();

                            if (corporateGetListPrefixAvailableResponseDtoDataResult != null)
                            {
                                if (corporateGetListPrefixAvailableResponseDtoDataResult.Success)
                                {
                                    if (corporateGetListPrefixAvailableResponseDtoDataResult.Data != null)
                                    {
                                        if (corporateGetListPrefixAvailableResponseDtoDataResult.Data.Count() > 0)
                                        {
                                            var corporateGetListPrefixAvailableResponseDtos = corporateGetListPrefixAvailableResponseDtoDataResult.Data;//get all corporates

                                            foreach (var corporateGetListPrefixAvailableResponseDto in corporateGetListPrefixAvailableResponseDtos)
                                            {
                                                var corporateId = corporateGetListPrefixAvailableResponseDto.Id;
                                                var corporateCode = corporateGetListPrefixAvailableResponseDto.CorporateCode;
                                                var corporateName = corporateGetListPrefixAvailableResponseDto.CorporateName;
                                                var prefix = corporateGetListPrefixAvailableResponseDto.Prefix;

                                                try
                                                {
                                                    var directoryDataResult = await _sftpService.GetDirectory(prefix);
                                                    if (directoryDataResult != null)
                                                    {
                                                        if (directoryDataResult.Success)
                                                        {
                                                            if (directoryDataResult.Data != null)
                                                            {
                                                                var directory = directoryDataResult.Data;//get corporate directory

                                                                var fileNamesDataResult = await _sftpService.GetFileNames(directory);
                                                                if (fileNamesDataResult != null)
                                                                {
                                                                    if (fileNamesDataResult.Success)
                                                                    {
                                                                        if (fileNamesDataResult.Data != null)
                                                                        {
                                                                            if (fileNamesDataResult.Data.Count() > 0)
                                                                            {
                                                                                var fileNames = fileNamesDataResult.Data;//get directory waiting files

                                                                                foreach (var fileName in fileNames)
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        var doFileWorkResponseDtoResult = await DoFileWork(corporateGetListPrefixAvailableResponseDto, user, fileName);
                                                                                        if (doFileWorkResponseDtoResult != null)
                                                                                        {
                                                                                            if (doFileWorkResponseDtoResult.Success)
                                                                                            {
                                                                                                if (doFileWorkResponseDtoResult.Data != null)
                                                                                                {
                                                                                                    var doFileWorkResponse = doFileWorkResponseDtoResult.Data;
                                                                                                    var smtpSuccessMailResponseDto = new SmtpSuccessMailResponseDto
                                                                                                    {
                                                                                                        CorporateCode = corporateCode,
                                                                                                        CorporateName = corporateName,
                                                                                                        FileName = fileName,
                                                                                                        Count = doFileWorkResponse.Count,
                                                                                                        TotalAmount = doFileWorkResponse.TotalAmount
                                                                                                    };
                                                                                                    smtpSuccessMailResponseDtos.Add(smtpSuccessMailResponseDto);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception ex)
                                                                                    {
                                                                                        var smtpErrorMailResponseDto = new SmtpErrorMailResponseDto
                                                                                        {
                                                                                            CorporateCode = corporateCode,
                                                                                            CorporateName = corporateName,
                                                                                            FileName = fileName,
                                                                                            Exception = ex.ToString()
                                                                                        };
                                                                                        smtpErrorMailResponseDtos.Add(smtpErrorMailResponseDto);
                                                                                        continue;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    var smtpErrorMailResponseDto = new SmtpErrorMailResponseDto
                                                    {
                                                        CorporateCode = corporateCode,
                                                        CorporateName = corporateName,
                                                        FileName = "",
                                                        Exception = ex.ToString()
                                                    };
                                                    smtpErrorMailResponseDtos.Add(smtpErrorMailResponseDto);
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var smtpStopMailResponseDto = new SmtpStopMailResponseDto
            {
                StopDate = DateTime.Now,
                ProcessingTimeSeconds = _stopwatch.Elapsed.TotalSeconds
            };

            var smtpMailResponseDto = new SmtpMailResponseDto
            {
                SmtpStartMailResponseDtos = smtpStartMailResponseDto,
                SmtpSuccessMailResponseDtos = smtpSuccessMailResponseDtos,
                SmtpErrorMailResponseDtos = smtpErrorMailResponseDtos,
                SmtpStopMailResponseDto = smtpStopMailResponseDto
            };

            try
            {
                await PrepareMail(smtpMailResponseDto);
            }
            catch (Exception)
            {
            }
            _stopwatch.Reset();
            return new SuccessResult(WorkerMessages.OperationSuccess);
        }

        [SecurityAspect("Window.DoFileWork", Priority = 2)]
        [TransactionAspect(Priority = 3)]
        public async Task<IDataResult<DoFileWorkResponseDto>> DoFileWork(CorporateGetListPrefixAvailableResponseDto corporateGetListPrefixAvailableResponseDto, User user, string fileName)
        {
            var corporateId = corporateGetListPrefixAvailableResponseDto.Id;
            var corporateCode = corporateGetListPrefixAvailableResponseDto.CorporateCode;
            var moneyType = corporateGetListPrefixAvailableResponseDto.MoneyType;
            var prefix = corporateGetListPrefixAvailableResponseDto.Prefix;
            var comission = corporateGetListPrefixAvailableResponseDto.Comission;
            var comissionType = corporateGetListPrefixAvailableResponseDto.ComissionType;
            var comissionAccountNo = corporateGetListPrefixAvailableResponseDto.ComissionAccountNo;
            var requestNumber = "";


            var sftpDownloadFileRequestDto = new SftpDownloadFileRequestDto
            {
                Prefix = prefix,
                SftpFileName = fileName,
            };

            var sftpDownloadResult = await _sftpService.SftpDownload(sftpDownloadFileRequestDto);//file download from sftp to api

            if (sftpDownloadResult != null)
            {
                if (sftpDownloadResult.Success)
                {
                    var paymentRequestDownloadRequestDto = new PaymentRequestDownloadRequestDto
                    {
                        CorporateId = corporateId,
                        SftpFileName = fileName,
                    };

                    var paymentRequestDownloadResponseDtoDataResult = await _paymentRequestService.PaymentRequestDownload(paymentRequestDownloadRequestDto);

                    if (paymentRequestDownloadResponseDtoDataResult != null)
                    {
                        if (paymentRequestDownloadResponseDtoDataResult.Success)
                        {
                            if (paymentRequestDownloadResponseDtoDataResult.Data != null)
                            {
                                var paymentRequestDownloadResponseDto = paymentRequestDownloadResponseDtoDataResult.Data;//readed file data

                                if (paymentRequestDownloadResponseDto.PaymentRequest != null)
                                {
                                    var paymentRequest = paymentRequestDownloadResponseDto.PaymentRequest;//readed data PaymentRequest

                                    requestNumber = paymentRequest.RequestNumber;

                                    var sftpDownloadGetBySftpFileNameResponseDtoDataResult = await _sftpDownloadService.GetBySftpFileName(fileName);
                                    if (sftpDownloadGetBySftpFileNameResponseDtoDataResult != null)
                                    {
                                        if (sftpDownloadGetBySftpFileNameResponseDtoDataResult.Success)
                                        {
                                            if (sftpDownloadGetBySftpFileNameResponseDtoDataResult.Data != null)
                                            {
                                                var sftpDownloadGetBySftpFileNameResponseDto = sftpDownloadGetBySftpFileNameResponseDtoDataResult.Data;
                                                var fileId = sftpDownloadGetBySftpFileNameResponseDto.Id;

                                                paymentRequest.FileId = fileId;

                                                if (paymentRequestDownloadResponseDto.PaymentRequestDetails != null)
                                                {
                                                    if (paymentRequestDownloadResponseDto.PaymentRequestDetails.Count() > 0)
                                                    {
                                                        var paymentRequestDetails = paymentRequestDownloadResponseDto.PaymentRequestDetails;//readed data PaymentRequestDetails

                                                        decimal totalAmount = 0;

                                                        int count = paymentRequestDetails.Count();

                                                        foreach (var paymentRequestDetail in paymentRequestDetails)
                                                        {
                                                            totalAmount += paymentRequestDetail.PaymentAmount;//readed file total
                                                        }

                                                        var paymentRequestDetailCommisyon = new PaymentRequestDetail //create paymentRequestDetailCommisyon 
                                                        {
                                                            ReferenceNumber = requestNumber + "-Com",
                                                            AccountNumber = comissionAccountNo,
                                                            CustomerNumber = 0,
                                                            PhoneNumber = "",
                                                            FirstName = "",
                                                            LastName = "",
                                                            CardDepositDate = paymentRequestDetails[0].CardDepositDate
                                                        };

                                                        if (comissionType == ComissionTypeEnum.Percentage)
                                                        {
                                                            paymentRequestDetailCommisyon.PaymentAmount = ((decimal)0.01) * (comission * totalAmount); //set payment amount
                                                        }

                                                        if (comissionType == ComissionTypeEnum.Quantity)
                                                        {
                                                            paymentRequestDetailCommisyon.PaymentAmount = comission * paymentRequestDetails.Count();//set payment amount
                                                        }

                                                        paymentRequestDetailCommisyon.Explanation = "Komisyon Ödeme Tutarı";//set explanation

                                                        paymentRequestDetails.Add(paymentRequestDetailCommisyon);//add paymentRequestDetailCommisyon to paymentRequestDetails

                                                        paymentRequestDownloadResponseDto.PaymentRequestDetails = paymentRequestDetails;//set new  paymentRequestDetails to paymentRequestDownloadResponseDto

                                                        var paymentExternals = new List<PaymentExternal>();

                                                        foreach (var paymentRequestDetail in paymentRequestDetails)
                                                        {
                                                            var paymentExternal = new PaymentExternal
                                                            {
                                                                AccountNumber = paymentRequestDetail.AccountNumber,
                                                                CardDepositDate = paymentRequestDetail.CardDepositDate,
                                                                CustomerNumber = paymentRequestDetail.CustomerNumber,
                                                                Explanation = paymentRequestDetail.Explanation,
                                                                FirstName = paymentRequestDetail.FirstName,
                                                                LastName = paymentRequestDetail.LastName,
                                                                PaymentAmount = paymentRequestDetail.PaymentAmount,
                                                                PhoneNumber = paymentRequestDetail.PhoneNumber,
                                                                ReferenceNumber = paymentRequestDetail.ReferenceNumber,
                                                            };

                                                            paymentExternals.Add(paymentExternal);
                                                        }

                                                        var tandemPaymentTransferRequestExternalDto = new TandemPaymentTransferRequestExternalDto
                                                        {
                                                            CorporateCode = corporateCode,
                                                            MoneyType = moneyType,
                                                            RegistrationNumber = user.RegistrationNumber,
                                                            RequestNumber = requestNumber,
                                                            PaymentExternals = paymentExternals
                                                        };

                                                        var tandemPaymentTransferResponseExternalDtoResult = await _tandemService.PaymentTransfer(tandemPaymentTransferRequestExternalDto);//sent file data to tandem

                                                        if (tandemPaymentTransferResponseExternalDtoResult != null)
                                                        {
                                                            if (tandemPaymentTransferResponseExternalDtoResult.Success)
                                                            {
                                                                if (tandemPaymentTransferResponseExternalDtoResult.Data != null)
                                                                {
                                                                    var paymentRequestSaveRequestDto = new PaymentRequestSaveRequestDto
                                                                    {
                                                                        PaymentRequest = paymentRequest,
                                                                        PaymentRequestDetails = paymentRequestDetails,
                                                                        SaveType = SaveTypeEnum.Add
                                                                    };

                                                                    var saveResult = await _paymentRequestService.Save(paymentRequestSaveRequestDto);
                                                                    if (saveResult != null)
                                                                    {
                                                                        if (saveResult.Success)
                                                                        {
                                                                            var doFileWorkResponseDto = new DoFileWorkResponseDto
                                                                            {
                                                                                Count = count,
                                                                                TotalAmount = totalAmount
                                                                            };
                                                                            return new SuccessDataResult<DoFileWorkResponseDto>(doFileWorkResponseDto, WorkerMessages.OperationSuccess);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return new ErrorDataResult<DoFileWorkResponseDto>(null, WorkerMessages.OperationFailed);
        }
        public async Task<IResult> PrepareMail(SmtpMailResponseDto smtpMailResponseDto)
        {
            var smtpPttMailResponseDto = new SmtpPttMailResponseDto
            {
                SmtpStartMailResponseDto = smtpMailResponseDto.SmtpStartMailResponseDtos,
                SmtpSuccessMailResponseDtos = smtpMailResponseDto.SmtpSuccessMailResponseDtos,
                SmtpErrorMailResponseDtos = smtpMailResponseDto.SmtpErrorMailResponseDtos,
                SmtpStopMailResponseDto = smtpMailResponseDto.SmtpStopMailResponseDto
            };

            try
            {
                await SendMailPtt(smtpPttMailResponseDto);
            }
            catch (Exception)
            {
            }

            var corporateGetListPrefixAvailableResponseDtoDataResult = await _corporateService.GetListPrefixAvailable();
            if (corporateGetListPrefixAvailableResponseDtoDataResult != null)
            {
                if (corporateGetListPrefixAvailableResponseDtoDataResult.Success)
                {
                    if (corporateGetListPrefixAvailableResponseDtoDataResult.Data != null)
                    {
                        var corporateGetListPrefixAvailableResponseDtos = corporateGetListPrefixAvailableResponseDtoDataResult.Data;

                        foreach (var corporateGetListPrefixAvailableResponseDto in corporateGetListPrefixAvailableResponseDtos)
                        {
                            var smtpSuccessMailResponseDtos = smtpMailResponseDto.SmtpSuccessMailResponseDtos.Where(x => x.CorporateCode == corporateGetListPrefixAvailableResponseDto.CorporateCode).ToList();
                            var smtpErrorMailResponseDtos = smtpMailResponseDto.SmtpErrorMailResponseDtos.Where(x => x.CorporateCode == corporateGetListPrefixAvailableResponseDto.CorporateCode).ToList();

                            if (smtpSuccessMailResponseDtos.Count() > 0 || smtpErrorMailResponseDtos.Count() > 0)
                            {
                                var smtpCorporateMailResponseDto = new SmtpCorporateMailResponseDto
                                {
                                    CorporateCode = corporateGetListPrefixAvailableResponseDto.CorporateCode,
                                    CorporateName = corporateGetListPrefixAvailableResponseDto.CorporateName,
                                    SmtpSuccessMailResponseDtos = smtpSuccessMailResponseDtos,
                                    SmtpErrorMailResponseDtos = smtpErrorMailResponseDtos
                                };
                                try
                                {
                                    await SendMailCorporate(smtpCorporateMailResponseDto);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            return new SuccessResult(WorkerMessages.OperationSuccess);
        }

        public async Task<IResult> SendMailPtt(SmtpPttMailResponseDto smtpPttMailResponseDto)
        {
            var smtpStartMailResponseDto = smtpPttMailResponseDto.SmtpStartMailResponseDto;
            var smtpSuccessMailResponseDtos = smtpPttMailResponseDto.SmtpSuccessMailResponseDtos;
            var smtpErrorMailResponseDtos = smtpPttMailResponseDto.SmtpErrorMailResponseDtos;
            var smtpStopMailResponseDto = smtpPttMailResponseDto.SmtpStopMailResponseDto;

            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html>");

            sb.Append("<html>");

            sb.Append("<style>table, th, td {border: 1px solid black;}</style>");

            sb.Append("<thead>");
            sb.Append("<title>EPR Bilgilendirme</title>");
            sb.Append("</thead>");

            sb.Append("<tbody>");
            sb.Append("<h2 style='text-align:center'> EPR Bilgilendirme </h2>");

            //start
            if (smtpStartMailResponseDto != null)
            {
                sb.Append("<h3 style='text-align:left'>İşlem Başlangıcı</h3>");

                sb.Append("<table style='width:25%'>");
                sb.Append("<tr>");
                sb.Append("<th>Başlangıç Tarihi</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td>" + smtpStartMailResponseDto.StartDate + "</td>");

                sb.Append("</tr>");
                sb.Append("</table>");
            }

            //success
            if (smtpSuccessMailResponseDtos != null)
            {
                if (smtpSuccessMailResponseDtos.Count() > 0)
                {
                    sb.Append("<h3 style='text-align:left'>Başarılı İşlemler</h3>");

                    sb.Append("<table style='width:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<th>Kurum Kodu</th>");
                    sb.Append("<th>Kurum Adı</th>");
                    sb.Append("<th>Dosya Adı</th>");
                    sb.Append("<th>Adet</th>");
                    sb.Append("<th>Toplam Tutar</th>");
                    sb.Append("</tr>");

                    foreach (var smtpSuccessMailResponseDto in smtpSuccessMailResponseDtos)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.CorporateCode.ToString() + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.CorporateName + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.FileName + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.Count.ToString() + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.TotalAmount.ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }
            }

            //error
            if (smtpErrorMailResponseDtos != null)
            {
                if (smtpErrorMailResponseDtos.Count() > 0)
                {
                    sb.Append("<h3 style='text-align:left'>Hatalı İşlemler</h3>");

                    sb.Append("<table style='width:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<th>Kurum Kodu</th>");
                    sb.Append("<th>Kurum Adı</th>");
                    sb.Append("<th>Dosya Adı</th>");
                    sb.Append("<th>Hata</th>");
                    sb.Append("</tr>");

                    foreach (var smtpErrorMailResponseDto in smtpErrorMailResponseDtos)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.CorporateCode.ToString() + "</td>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.CorporateName + "</td>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.FileName + "</td>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.Exception + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }
            }

            //stop
            if (smtpStopMailResponseDto != null)
            {
                sb.Append("<h3 style='text-align:left'>İşlem Bitişi</h3>");

                sb.Append("<table style='width:50%'>");
                sb.Append("<tr>");
                sb.Append("<th>Bitiş Tarihi</th>");
                sb.Append("<th>İşlem Süresi</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td>" + smtpStopMailResponseDto.StopDate.ToString() + "</td>");
                sb.Append("<td>" + smtpStopMailResponseDto.ProcessingTimeSeconds.ToString() + " s</td>");

                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("</tbody>");
                sb.Append("</html>");
            }

            var mailAddressGetListPttResposeDtosDataResult = await _mailAddressService.GetListPtt();
            if (mailAddressGetListPttResposeDtosDataResult != null)
            {
                if (mailAddressGetListPttResposeDtosDataResult.Success)
                {
                    if (mailAddressGetListPttResposeDtosDataResult.Data != null)
                    {
                        if (mailAddressGetListPttResposeDtosDataResult.Data.Count() > 0)
                        {
                            var tos = new List<string>();
                            var ccs = new List<string>();
                            var mailAddressGetListPttResposeDtos = mailAddressGetListPttResposeDtosDataResult.Data;
                            foreach (var mailAddressGetListPttResposeDto in mailAddressGetListPttResposeDtos)
                            {
                                if (mailAddressGetListPttResposeDto.IsCC == true)
                                {
                                    ccs.Add(mailAddressGetListPttResposeDto.Address);
                                }
                                else
                                {
                                    tos.Add(mailAddressGetListPttResposeDto.Address);
                                }
                            }

                            var smtpSendRequestSuccessDto = new SmtpSendRequestDto
                            {
                                Body = sb.ToString(),
                                ToCCMailAddresses = ccs,
                                ToMailAddresses = tos
                            };
                            try
                            {
                                await _smtpService.SendSmtpMail(smtpSendRequestSuccessDto);
                            }
                            catch (Exception)
                            {
                            }
                            return new SuccessResult(WorkerMessages.OperationSuccess);
                        }
                    }
                }
            }
            return new ErrorResult(WorkerMessages.OperationFailed);
        }

        public async Task<IResult> SendMailCorporate(SmtpCorporateMailResponseDto smtpCorporateMailResponseDto)
        {
            var smtpSuccessMailResponseDtos = smtpCorporateMailResponseDto.SmtpSuccessMailResponseDtos;
            var smtpErrorMailResponseDtos = smtpCorporateMailResponseDto.SmtpErrorMailResponseDtos;
            var corporateCode = smtpCorporateMailResponseDto.CorporateCode;
            var corporateName = smtpCorporateMailResponseDto.CorporateName;

            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html>");

            sb.Append("<html>");

            sb.Append("<style>table, th, td {border: 1px solid black;}</style>");

            sb.Append("<thead>");
            sb.Append("<title>EPR Bilgilendirme</title>");
            sb.Append("</thead>");

            sb.Append("<tbody>");
            sb.Append("<h2 style='text-align:center'> EPR Bilgilendirme (" + corporateCode.ToString() + " - " + corporateName + ") </h2>");

            //success
            if (smtpSuccessMailResponseDtos != null)
            {
                if (smtpSuccessMailResponseDtos.Count() > 0)
                {
                    sb.Append("<h3 style='text-align:left'>Başarılı İşlemler</h3>");

                    sb.Append("<table style='width:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<th>Kurum Kodu</th>");
                    sb.Append("<th>Kurum Adı</th>");
                    sb.Append("<th>Dosya Adı</th>");
                    sb.Append("<th>Adet</th>");
                    sb.Append("<th>Toplam Tutar</th>");
                    sb.Append("</tr>");

                    foreach (var smtpSuccessMailResponseDto in smtpSuccessMailResponseDtos)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.CorporateCode.ToString() + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.CorporateName + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.FileName + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.Count.ToString() + "</td>");
                        sb.Append("<td>" + smtpSuccessMailResponseDto.TotalAmount.ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }
            }

            //error
            if (smtpErrorMailResponseDtos != null)
            {
                if (smtpErrorMailResponseDtos.Count() > 0)
                {
                    sb.Append("<h3 style='text-align:left'>Hatalı İşlemler</h3>");

                    sb.Append("<table style='width:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<th>Kurum Kodu</th>");
                    sb.Append("<th>Kurum Adı</th>");
                    sb.Append("<th>Dosya Adı</th>");
                    sb.Append("<th>Hata</th>");
                    sb.Append("</tr>");

                    foreach (var smtpErrorMailResponseDto in smtpErrorMailResponseDtos)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.CorporateCode.ToString() + "</td>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.CorporateName + "</td>");
                        sb.Append("<td>" + smtpErrorMailResponseDto.FileName + "</td>");
                        sb.Append("<td>Bir Hata Oluştu.Lütfen PTT ile iletişime Geçiniz</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }
            }

            sb.Append("</tbody>");
            sb.Append("</html>");

            var corporateMailAddressGetListByCorporateIdResponseDtosDataResult = await _corporateMailAddressService.GetByCorporateId(corporateCode);
            if (corporateMailAddressGetListByCorporateIdResponseDtosDataResult != null)
            {
                if (corporateMailAddressGetListByCorporateIdResponseDtosDataResult.Success)
                {
                    if (corporateMailAddressGetListByCorporateIdResponseDtosDataResult.Data != null)
                    {
                        if (corporateMailAddressGetListByCorporateIdResponseDtosDataResult.Data.Count() > 0)
                        {
                            var tos = new List<string>();
                            var ccs = new List<string>();
                            var corporateMailAddressGetListByCorporateIdResponseDtos = corporateMailAddressGetListByCorporateIdResponseDtosDataResult.Data;
                            foreach (var corporateMailAddressGetListByCorporateIdResponseDto in corporateMailAddressGetListByCorporateIdResponseDtos)
                            {
                                if (corporateMailAddressGetListByCorporateIdResponseDto.IsCC == true)
                                {
                                    ccs.Add(corporateMailAddressGetListByCorporateIdResponseDto.Address);
                                }
                                else
                                {
                                    tos.Add(corporateMailAddressGetListByCorporateIdResponseDto.Address);
                                }
                            }

                            var smtpSendRequestSuccessDto = new SmtpSendRequestDto
                            {
                                Body = sb.ToString(),
                                ToCCMailAddresses = ccs,
                                ToMailAddresses = tos
                            };
                            try
                            {
                                await _smtpService.SendSmtpMail(smtpSendRequestSuccessDto);
                            }
                            catch (Exception)
                            {
                            }
                            return new SuccessResult(WorkerMessages.OperationSuccess);
                        }
                    }
                }
            }
            return new ErrorResult(WorkerMessages.OperationFailed);
        }

    }
}
