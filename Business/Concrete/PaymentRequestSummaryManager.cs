using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.MenuValidators;
using Business.ValidationRules.FluentValidation.PaymentRequestSummaryValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAddDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAmountByCorporateIdYearDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryChartByCorporateIdYear;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetByIdDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetListByPaymentRequestIdDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySaveDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySearchDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryUpdateDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestTotalOutgoingPaymentDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums;
using Entities.Concrete.Enums.GeneralEnums;
using Tandem.Abstract;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class PaymentRequestSummaryManager : IPaymentRequestSummaryService
    {
        private IMapper _mapper;
        private IPaymentRequestSummaryDal _paymentRequestSummaryDal;
        private IPaymentRequestDal _paymentRequestDal;
        private ISftpDownloadDal _sftpDownloadDal;
        private IHcpUploadDal _hcpUploadDal;
        public PaymentRequestSummaryManager(IMapper mapper,
            IPaymentRequestSummaryDal paymentRequestSummaryDal,
            IPaymentRequestDal paymentRequestDal,
            ISftpDownloadDal sftpDownloadDal,
            IHcpUploadDal hcpUploadDal
            )
        {
            _mapper = mapper;
            _paymentRequestSummaryDal = paymentRequestSummaryDal;
            _paymentRequestDal = paymentRequestDal;
            _sftpDownloadDal = sftpDownloadDal;
            _hcpUploadDal = hcpUploadDal;
        }

        [SecurityAspect("PaymentRequestSummary.Add", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSummaryAddRequestDtoValidator), Priority = 3)]
        public async Task<IResult> Add(PaymentRequestSummaryAddRequestDto paymentRequestSummaryAddRequestDto)
        {
            var paymentRequestSummary = _mapper.Map<PaymentRequestSummary>(paymentRequestSummaryAddRequestDto);
            await _paymentRequestSummaryDal.Add(paymentRequestSummary);
            return new SuccessResult(PaymentRequestSummaryMessages.Added);

        }

        [SecurityAspect("PaymentRequestSummary.Delete", Priority = 2)]
        public async Task<IResult> Delete(int id)
        {
            var paymentRequestSummary = await _paymentRequestSummaryDal.Get(x => x.Id == id && x.IsActive == true);
            if (paymentRequestSummary != null)
            {
                paymentRequestSummary.IsActive = false;
                await _paymentRequestSummaryDal.Update(paymentRequestSummary);

            }
            return new SuccessResult(PaymentRequestSummaryMessages.Deleted);
        }

        [SecurityAspect("PaymentRequestSummary.GetById", Priority = 2)]
        public async Task<IDataResult<PaymentRequestSummaryGetByIdResponseDto>> GetById(int id)
        {
            var paymentRequestSummary = await _paymentRequestSummaryDal.Get(x => x.Id == id && x.IsActive == true);
            var paymentRequestSummaryGetByIdResponseDto = _mapper.Map<PaymentRequestSummaryGetByIdResponseDto>(paymentRequestSummary);
            return new SuccessDataResult<PaymentRequestSummaryGetByIdResponseDto>(paymentRequestSummaryGetByIdResponseDto);
        }

        [SecurityAspect("PaymentRequestSummary.GetList", Priority = 2)]
        public async Task<IDataResult<List<PaymentRequestSummaryGetListByRequestIdResponseDto>>> GetList()
        {
            var paymentRequestSummary = await _paymentRequestSummaryDal.GetList(x => x.IsActive == true);
            var paymentRequestSummaryGetListResponseDto = _mapper.Map<List<PaymentRequestSummaryGetListByRequestIdResponseDto>>(paymentRequestSummary);
            paymentRequestSummaryGetListResponseDto = paymentRequestSummaryGetListResponseDto.OrderBy(x => x.Optime).ToList();
            return new SuccessDataResult<List<PaymentRequestSummaryGetListByRequestIdResponseDto>>(paymentRequestSummaryGetListResponseDto);
        }

        [SecurityAspect("PaymentRequestSummary.GetListByPaymentRequestId", Priority = 2)]
        public async Task<IDataResult<List<PaymentRequestSummaryGetListByPaymentRequestIdResponseDto>>> GetListByPaymentRequestId(int paymentRequestId)
        {
            var paymentRequestSummary = await _paymentRequestSummaryDal.GetList(x => x.PaymentRequestId == paymentRequestId && x.IsActive == true);
            var paymentRequestSummaryGetListByPaymentRequestIdResponseDto = _mapper.Map<List<PaymentRequestSummaryGetListByPaymentRequestIdResponseDto>>(paymentRequestSummary);
            paymentRequestSummaryGetListByPaymentRequestIdResponseDto = paymentRequestSummaryGetListByPaymentRequestIdResponseDto.OrderBy(x => x.Optime).ToList();
            return new SuccessDataResult<List<PaymentRequestSummaryGetListByPaymentRequestIdResponseDto>>(paymentRequestSummaryGetListByPaymentRequestIdResponseDto);
        }

        [SecurityAspect("PaymentRequestSummary.Update", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSummaryUpdateRequestDtoValidator), Priority = 3)]
        public async Task<IResult> Update(PaymentRequestSummaryUpdateRequestDto paymentRequestSummaryUpdateRequestDto)
        {
            var paymentRequestSummary = _mapper.Map<PaymentRequestSummary>(paymentRequestSummaryUpdateRequestDto);
            if (paymentRequestSummary != null)
            {
                await _paymentRequestSummaryDal.Update(paymentRequestSummary);
                return new SuccessResult(PaymentRequestSummaryMessages.Updated);

            }
            return new ErrorResult(PaymentRequestSummaryMessages.OperationFailed);

        }

        [SecurityAspect("PaymentRequestSummary.Save", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSummarySaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IPaymentRequestSummaryService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(PaymentRequestSummarySaveRequestDto paymentRequestSummarySaveRequestDto)
        {
            var saveType = paymentRequestSummarySaveRequestDto.SaveType;
            var paymentRequestSummaries = paymentRequestSummarySaveRequestDto.PaymentRequestSummaries;
            var requestNumber = paymentRequestSummarySaveRequestDto.RequestNumber;
            var hcpFileRequests = paymentRequestSummarySaveRequestDto.HcpFileRequests;
            if (saveType == SaveTypeEnum.Add)
            {

                foreach (var paymentRequestSummary in paymentRequestSummaries)
                {
                    var paymentRequest = await _paymentRequestDal.Get(x => x.RequestNumber == requestNumber && x.IsActive == true);
                    if (paymentRequest != null)
                    {
                        paymentRequest.Status = paymentRequestSummary.Status;
                        await _paymentRequestDal.Update(paymentRequest);

                        if(hcpFileRequests != null)
                        {
                            if(hcpFileRequests.Count()>0)
                            {
                                foreach(var hcpFileRequest in hcpFileRequests)
                                {
                                    var hcpUploadAdd = new HcpUpload
                                    {
                                        PaymentRequestId = paymentRequest.Id,
                                        HcpId = hcpFileRequest.HcpId,
                                        Extension = hcpFileRequest.Extension,
                                        Explanation = hcpFileRequest.Explanation,
                                        IsActive = true,
                                        Optime = DateTime.Now
                                    };
                                    await _hcpUploadDal.Add(hcpUploadAdd);
                                }
                            }
                        }
                     

                        var sftpDownload = await _sftpDownloadDal.Get(x => x.Id == paymentRequest.FileId && x.IsActive == true);
                        if (sftpDownload != null)
                        {
                            sftpDownload.Status = paymentRequestSummary.Status;
                            sftpDownload.UserId = paymentRequestSummary.UserId;
                            sftpDownload.Optime = DateTime.Now;
                            await _sftpDownloadDal.Update(sftpDownload);
                        }

                        paymentRequestSummary.PaymentRequestId = paymentRequest.Id;
                        paymentRequestSummary.IsActive = true;
                        paymentRequestSummary.Optime = DateTime.Now;
                        await _paymentRequestSummaryDal.Add(paymentRequestSummary);
                    }
                }

                return new SuccessResult(PaymentRequestSummaryMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                foreach (var paymentRequestSummary in paymentRequestSummaries)
                {
                    var paymentRequest = await _paymentRequestDal.Get(x => x.Id == paymentRequestSummary.Id && x.IsActive == true);
                    if (paymentRequest != null)
                    {
                        paymentRequest.Status = paymentRequestSummary.Status;
                        await _paymentRequestDal.Update(paymentRequest);

                        if (hcpFileRequests != null)
                        {
                            if (hcpFileRequests.Count() > 0)
                            {
                                foreach (var hcpFileRequest in hcpFileRequests)
                                {
                                    var hcpUploadAdd = new HcpUpload
                                    {
                                        PaymentRequestId = paymentRequest.Id,
                                        HcpId = hcpFileRequest.HcpId,
                                        Extension = hcpFileRequest.Extension,
                                        Explanation = hcpFileRequest.Explanation,
                                        IsActive = true,
                                        Optime = DateTime.Now
                                    };
                                    await _hcpUploadDal.Add(hcpUploadAdd);
                                }
                            }
                        }

                        var sftpDownload = await _sftpDownloadDal.Get(x => x.Id == paymentRequest.FileId && x.IsActive == true);
                        if (sftpDownload != null)
                        {
                            sftpDownload.Status = paymentRequestSummary.Status;
                            sftpDownload.UserId = paymentRequestSummary.UserId;
                            sftpDownload.Optime = DateTime.Now;

                            await _sftpDownloadDal.Update(sftpDownload);
                        }

                        paymentRequestSummary.PaymentRequestId = paymentRequest.Id;
                        paymentRequestSummary.IsActive = true;
                        paymentRequestSummary.Optime = DateTime.Now;
                        await _paymentRequestSummaryDal.Add(paymentRequestSummary);
                    }
                }
                return new SuccessResult(PaymentRequestSummaryMessages.Updated);
            }
            return new ErrorResult(PaymentRequestSummaryMessages.OperationFailed);
        }

        [SecurityAspect("PaymentRequestSummary.Search", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSummarySearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<PaymentRequestSummarySearchResponseDto>>> Search(PaymentRequestSummarySearchRequestDto paymentRequestSummarySearchRequestDto)
        {
            return new SuccessDataResult<List<PaymentRequestSummarySearchResponseDto>>(await _paymentRequestSummaryDal.Search(paymentRequestSummarySearchRequestDto));
        }

        [SecurityAspect("PaymentRequestSummary.AmountByCorporateIdYear", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestSummaryAmountByCorporateIdYearRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<PaymentRequestSummaryAmountByCorporateIdYearResponseDto>>> AmountByCorporateIdYear(PaymentRequestSummaryAmountByCorporateIdYearRequestDto paymentRequestSummaryAmountByCorporateIdYearRequestDto)
        {
            return new SuccessDataResult<List<PaymentRequestSummaryAmountByCorporateIdYearResponseDto>>(await _paymentRequestSummaryDal.AmountByCorporateIdYear(paymentRequestSummaryAmountByCorporateIdYearRequestDto));
        }

        [SecurityAspect("PaymentRequestSummary.GetByToday", Priority = 2)]
        public async Task<IDataResult<List<PaymentRequestSummaryGetByTodayResponseDto>>> GetByToday()
        {
            return new SuccessDataResult<List<PaymentRequestSummaryGetByTodayResponseDto>>(await _paymentRequestSummaryDal.GetByToday());
        }

        [SecurityAspect("PaymentRequestSummary.TotalOutgoingPayment", Priority = 2)]
        public async Task<IDataResult<List<PaymentRequestSummaryTotalOutgoingPaymentResponseDto>>> TotalOutgoingPayment()
        {
            return new SuccessDataResult<List<PaymentRequestSummaryTotalOutgoingPaymentResponseDto>>(await _paymentRequestSummaryDal.TotalOutgoingPayment());
        }
    }
}
