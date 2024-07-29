using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.PaymentRequestValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSaveDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetListDtos;
using Entities.Concrete.Enums.GeneralEnums;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestUpdateDtos;
using Business.ValidationRules.FluentValidation.MenuValidators;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using System.Data;
using Core.HttpAccessor.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos;
using ExcelDataReader;
using Entities.Concrete.Enums;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByRequestNumberDtos;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System.Globalization;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetListByCorporateIdDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class PaymentRequestManager : IPaymentRequestService
    {
        private ICorporateDal _corporateDal;
        private IUserDal _userDal;
        private IPaymentRequestDal _paymentRequestDal;
        private IPaymentRequestDetailDal _paymentRequestDetailDal;
        private ISftpDownloadDal _sftpDownloadDal;
        private IMapper _mapper;
        private IHttpContextAccessorService _httpContextAccessorService;

        public PaymentRequestManager(
            ICorporateDal corporateDal,
            IUserDal userDal,
            IPaymentRequestDal paymentRequestDal,
            IPaymentRequestDetailDal paymentRequestDetailDal,
            ISftpDownloadDal sftpDownloadDal,
            IMapper mapper,
            IHttpContextAccessorService httpContextAccessorService
            )
        {
            _corporateDal = corporateDal;
            _userDal = userDal;
            _mapper = mapper;
            _paymentRequestDal = paymentRequestDal;
            _paymentRequestDetailDal = paymentRequestDetailDal;
            _sftpDownloadDal = sftpDownloadDal;
            _httpContextAccessorService = httpContextAccessorService;
        }

        [SecurityAspect("PaymentRequest.Add", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IPaymentRequestService.Get", Priority = 4)]
        public async Task<IResult> Add(PaymentRequestAddRequestDto paymentRequestAddRequestDto)
        {
            var paymentRequest = _mapper.Map<PaymentRequest>(paymentRequestAddRequestDto);
            await _paymentRequestDal.Add(paymentRequest);
            return new SuccessResult(PaymentRequestMessages.Added);
        }

        [SecurityAspect("PaymentRequest.Delete", Priority = 2)]
        [CacheRemoveAspect("IPaymentRequestService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var paymentRequest = await _paymentRequestDal.Get(x => x.Id == id && x.IsActive == true);
            if (paymentRequest != null)
            {
                paymentRequest.IsActive = false;
                await _paymentRequestDal.Update(paymentRequest);
                return new SuccessResult(PaymentRequestMessages.Deleted);
            }
            return new ErrorResult(PaymentRequestMessages.OperationFailed);
        }

        [SecurityAspect("PaymentRequest.GetById", Priority = 2)]
        public async Task<IDataResult<PaymentRequestGetByIdResponseDto>> GetById(int id)
        {
            var paymentRequest = await _paymentRequestDal.Get(x => x.Id == id && x.IsActive == true);
            var paymentRequestGetByIdResponseDto = _mapper.Map<PaymentRequestGetByIdResponseDto>(paymentRequest);
            return new SuccessDataResult<PaymentRequestGetByIdResponseDto>(paymentRequestGetByIdResponseDto);
        }

        [SecurityAspect("PaymentRequest.GetByRequestNumber", Priority = 2)]
        public async Task<IDataResult<PaymentRequestGetByRequestNumberResponseDto>> GetByRequestNumber(string requestNumber)
        {
            var paymentRequest = await _paymentRequestDal.Get(x => x.RequestNumber == requestNumber && x.IsActive == true);
            var paymentRequestGetByRequestNumberResponseDto = _mapper.Map<PaymentRequestGetByRequestNumberResponseDto>(paymentRequest);
            return new SuccessDataResult<PaymentRequestGetByRequestNumberResponseDto>(paymentRequestGetByRequestNumberResponseDto);
        }

        [SecurityAspect("PaymentRequest.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<PaymentRequestGetListResponseDto>>> GetList()
        {
            var paymentRequests = await _paymentRequestDal.GetList(x => x.IsActive == true);
            var paymentRequestGetListResponseDtos = _mapper.Map<List<PaymentRequestGetListResponseDto>>(paymentRequests);
            paymentRequestGetListResponseDtos = paymentRequestGetListResponseDtos.OrderBy(x => x.RequestNumber).ToList();
            return new SuccessDataResult<List<PaymentRequestGetListResponseDto>>(paymentRequestGetListResponseDtos);
        }

        [SecurityAspect("PaymentRequest.Update", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IPaymentRequestService.Get", Priority = 4)]
        public async Task<IResult> Update(PaymentRequestUpdateRequestDto paymentRequestUpdateRequestDto)
        {
            var paymentRequest = _mapper.Map<PaymentRequest>(paymentRequestUpdateRequestDto);
            if (paymentRequest != null)
            {
                await _paymentRequestDal.Update(paymentRequest);
                return new SuccessResult(PaymentRequestMessages.Updated);
            }

            return new ErrorResult(PaymentRequestMessages.OperationFailed);
        }

        [SecurityAspect("PaymentRequest.Save", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IPaymentRequestService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(PaymentRequestSaveRequestDto paymentRequestSaveRequestDto)
        {
            var saveType = paymentRequestSaveRequestDto.SaveType;
            var paymentRequest = paymentRequestSaveRequestDto.PaymentRequest;
            var paymentRequestDetails = paymentRequestSaveRequestDto.PaymentRequestDetails;
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
                            if (saveType == SaveTypeEnum.Add)
                            {
                                paymentRequest.IsActive = true;
                                paymentRequest.Optime = DateTime.Now;
                                paymentRequest.Status = StatusEnum.SummaryWaitingForApproval;
                                paymentRequest.UserId = user.Id;

                                await _paymentRequestDal.Add(paymentRequest);

                                foreach (var paymentRequestDetail in paymentRequestDetails)
                                {
                                    paymentRequestDetail.IsActive = true;
                                    paymentRequestDetail.Optime = DateTime.Now;
                                    paymentRequestDetail.CardDepositDate = paymentRequestDetail.CardDepositDate;
                                    paymentRequestDetail.PaymentRequestId = paymentRequest.Id;

                                    await _paymentRequestDetailDal.Add(paymentRequestDetail);
                                }

                                var sftpDownload = await _sftpDownloadDal.Get(x => x.Id == paymentRequest.FileId && x.IsActive == true);

                                if (sftpDownload != null)
                                {
                                    sftpDownload.Status = StatusEnum.SummaryWaitingForApproval;
                                    sftpDownload.UserId = user.Id;
                                    sftpDownload.Optime = DateTime.Now;

                                    await _sftpDownloadDal.Update(sftpDownload);
                                }

                                return new SuccessResult(PaymentRequestMessages.Added);
                            }
                            else if (saveType == SaveTypeEnum.Update)
                            {
                                paymentRequest.IsActive = true;
                                paymentRequest.Optime = DateTime.Now;
                                paymentRequest.Status = StatusEnum.SummaryWaitingForApproval;
                                paymentRequest.UserId = user.Id;

                                await _paymentRequestDal.Update(paymentRequest);

                                foreach (var paymentRequestDetail in paymentRequestDetails)
                                {
                                    paymentRequestDetail.IsActive = true;
                                    paymentRequestDetail.Optime = DateTime.Now;
                                    paymentRequestDetail.CardDepositDate = paymentRequestDetail.CardDepositDate;
                                    paymentRequestDetail.PaymentRequestId = paymentRequest.Id;

                                    await _paymentRequestDetailDal.Update(paymentRequestDetail);
                                }

                                var sftpDownload = await _sftpDownloadDal.Get(x => x.Id == paymentRequest.FileId && x.IsActive == true);

                                if (sftpDownload != null)
                                {
                                    sftpDownload.Status = StatusEnum.SummaryWaitingForApproval;
                                    sftpDownload.UserId = user.Id;
                                    sftpDownload.Optime = DateTime.Now;

                                    await _sftpDownloadDal.Update(sftpDownload);
                                }

                                return new SuccessResult(PaymentRequestMessages.Updated);
                            }
                        }
                    }
                }
            }
            return new ErrorResult(PaymentRequestMessages.OperationFailed);
        }

        [SecurityAspect("PaymentRequest.Search", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<PaymentRequestSearchResponseDto>>> Search(PaymentRequestSearchRequestDto paymentRequestSearchRequestDto)
        {
            return new SuccessDataResult<List<PaymentRequestSearchResponseDto>>(await _paymentRequestDal.Search(paymentRequestSearchRequestDto));
        }

        [SecurityAspect("PaymentRequest.GetByToday", Priority = 2)]
        public async Task<IDataResult<List<PaymentRequestGetByTodayResponseDto>>> GetByToday()
        {
            return new SuccessDataResult<List<PaymentRequestGetByTodayResponseDto>>(await _paymentRequestDal.GetByToday());
        }

        [SecurityAspect("PaymentRequest.PaymentRequestDownload", Priority = 2)]
        [TransactionAspect(Priority = 3)]
        public async Task<IDataResult<PaymentRequestDownloadResponseDto>> PaymentRequestDownload(PaymentRequestDownloadRequestDto paymentRequestDownloadRequestDto)
        {
            CultureInfo cultures = new CultureInfo("tr-TR");

            var paymentRequestDownloadResponseDto = new PaymentRequestDownloadResponseDto();
            paymentRequestDownloadResponseDto.PaymentRequest = new PaymentRequest();
            paymentRequestDownloadResponseDto.PaymentRequestDetails = new List<PaymentRequestDetail>();

            if (paymentRequestDownloadRequestDto != null)
            {
                if (paymentRequestDownloadRequestDto.SftpFileName != null)
                {
                    if (paymentRequestDownloadRequestDto.CorporateId != null)
                    {
                        var sftpFileName = paymentRequestDownloadRequestDto.SftpFileName;
                        var corporateId = paymentRequestDownloadRequestDto.CorporateId;
                        var registrationNumberResult = await _httpContextAccessorService.GetRegistrationNumber();
                        if (registrationNumberResult != null)
                        {
                            if (registrationNumberResult.Success)
                            {
                                if (registrationNumberResult.Data != null)
                                {
                                    var registrationNumber = registrationNumberResult.Data;

                                    if (registrationNumber > 0)
                                    {
                                        var filePathLocal = "c:/sftp/" + registrationNumber.ToString() + sftpFileName;
                                        if (filePathLocal != null)
                                        {
                                            if (File.Exists(filePathLocal))
                                            {
                                                if (File.GetCreationTime(filePathLocal) >= DateTime.Today.AddDays(-1))
                                                {
                                                    FileStream stream = File.Open(filePathLocal, FileMode.Open, FileAccess.Read);
                                                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                                    var excelDtos = new List<ExcelDto>();
                                                    var rowId = 0;
                                                    while (excelReader.Read())
                                                    {
                                                        var excelDto = new ExcelDto
                                                        {
                                                            RowId = rowId,
                                                            ColumnZero = excelReader.GetValue(0),
                                                            ColumnOne = excelReader.GetValue(1),
                                                            ColumnTwo = excelReader.GetValue(2),
                                                            ColumnThree = excelReader.GetValue(3),
                                                            ColumnFour = excelReader.GetValue(4),
                                                            ColumnFive = excelReader.GetValue(5),
                                                            ColumnSix = excelReader.GetValue(6),
                                                            ColumnSeven = excelReader.GetValue(7),
                                                            ColumnEight = excelReader.GetValue(8),
                                                            ColumnNine = excelReader.GetValue(9),
                                                            ColumnTen = excelReader.GetValue(10),
                                                        };

                                                        excelDtos.Add(excelDto);

                                                        rowId += 1;
                                                    }
                                                    excelReader.Close();
                                                    if (excelDtos != null)
                                                    {
                                                        if (excelDtos.Count > 0)
                                                        {
                                                            var corporate = await _corporateDal.Get(x => x.Id == corporateId && x.IsActive == true);
                                                            if (corporate != null)
                                                            {
                                                                var user = await _userDal.Get(x => x.RegistrationNumber == registrationNumber && x.IsActive == true);
                                                                if (user != null)
                                                                {
                                                                    var requestNumber = Convert.ToString(excelDtos[2].ColumnFour);
                                                                    var paymentRequest = new PaymentRequest
                                                                    {
                                                                        CorporateId = corporate.Id,
                                                                        MoneyType = corporate.MoneyType,
                                                                        RequestNumber = requestNumber,
                                                                        Status = StatusEnum.DownloadedFromApi,
                                                                        UserId = user.Id
                                                                    };

                                                                    paymentRequestDownloadResponseDto.PaymentRequest = paymentRequest;
                                                                    if (paymentRequestDownloadResponseDto.PaymentRequest != null)
                                                                    {
                                                                        var paymentRequestDetails = new List<PaymentRequestDetail>();
                                                                        foreach (var excelDto in excelDtos)
                                                                        {
                                                                            if (excelDto.RowId > 4)
                                                                            {
                                                                                if (!string.IsNullOrEmpty(Convert.ToString(excelDto.ColumnOne)))
                                                                                {
                                                                                    var paymentRequestDetail = new PaymentRequestDetail
                                                                                    {
                                                                                        ReferenceNumber = Convert.ToString(excelDto.ColumnOne),
                                                                                        AccountNumber = Convert.ToInt64(excelDto.ColumnTwo),
                                                                                        CustomerNumber = Convert.ToInt64(excelDto.ColumnThree),
                                                                                        PhoneNumber = Convert.ToString(excelDto.ColumnFour),
                                                                                        FirstName = Convert.ToString(excelDto.ColumnFive),
                                                                                        LastName = Convert.ToString(excelDto.ColumnSix),
                                                                                        PaymentAmount = Convert.ToDecimal(excelDto.ColumnSeven, cultures),
                                                                                        CardDepositDate = Convert.ToDateTime(excelDto.ColumnNine, cultures),
                                                                                        Explanation = Convert.ToString(excelDto.ColumnTen),
                                                                                    };
                                                                                    paymentRequestDetails.Add(paymentRequestDetail);
                                                                               }
                                                                            }
                                                                        }

                                                                        paymentRequestDownloadResponseDto.PaymentRequestDetails = paymentRequestDetails;

                                                                        return new SuccessDataResult<PaymentRequestDownloadResponseDto>(paymentRequestDownloadResponseDto);
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
            return new ErrorDataResult<PaymentRequestDownloadResponseDto>(null);
        }

        [SecurityAspect("PaymentRequest.GetListByCorporateId", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<PaymentRequestGetListByCorporateIdResponseDto>>> GetListByCorporateId(int corporateId)
        {
            var paymentRequests = await _paymentRequestDal.GetList(x => x.CorporateId == corporateId && x.IsActive == true);
            var paymentRequestGetListByCorporateIdResponseDto = _mapper.Map<List<PaymentRequestGetListByCorporateIdResponseDto>>(paymentRequests);
            paymentRequestGetListByCorporateIdResponseDto = paymentRequestGetListByCorporateIdResponseDto.OrderBy(x => x.RequestNumber).ToList();
            return new SuccessDataResult<List<PaymentRequestGetListByCorporateIdResponseDto>>(paymentRequestGetListByCorporateIdResponseDto);
        }
    }
}
