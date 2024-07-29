using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.PaymentRequestDetailDetailValidators;
using Business.ValidationRules.FluentValidation.PaymentRequestDetailValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListByPaymentRequestIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailUpdateDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestGetByIdDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class PaymentRequestDetailManager : IPaymentRequestDetailService
    {
        private IPaymentRequestDetailDal _paymentRequestDetailDal;
        private IMapper _mapper;

        public PaymentRequestDetailManager(IPaymentRequestDetailDal paymentRequestDetailDal, IMapper mapper )
        {
            _mapper = mapper;
            _paymentRequestDetailDal = paymentRequestDetailDal;
        }

        [SecurityAspect("PaymentRequestDetail.Add", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestDetailAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IPaymentRequestDetailService.Get", Priority = 4)]
        public async Task<IResult> Add(PaymentRequestDetailAddRequestDto paymentRequestDetailAddRequestDto)
        {
            var paymentRequestDetail = _mapper.Map<PaymentRequestDetail>(paymentRequestDetailAddRequestDto);
            await _paymentRequestDetailDal.Add(paymentRequestDetail);
            return new SuccessResult(PaymentRequestDetailMessages.Added);
        }

        [SecurityAspect("PaymentRequestDetail.Delete", Priority = 2)]
        [CacheRemoveAspect("IPaymentRequestDetailService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var paymentRequestDetail = await _paymentRequestDetailDal.Get(x => x.Id == id && x.IsActive == true);
            if (paymentRequestDetail != null)
            {
                paymentRequestDetail.IsActive = false;
                await _paymentRequestDetailDal.Update(paymentRequestDetail);
                return new SuccessResult(PaymentRequestDetailMessages.Deleted);

            }

            return new ErrorResult(PaymentRequestDetailMessages.OperationFailed);
        }

        [SecurityAspect("PaymentRequestDetail.GetById", Priority = 2)]
        public async Task<IDataResult<PaymentRequestDetailGetByIdResponseDto>> GetById(int id)
        {
            var paymentRequestDetail = await _paymentRequestDetailDal.Get(x => x.Id == id && x.IsActive == true);
            var paymentRequestDetailGetByIdResponseDto = _mapper.Map<PaymentRequestDetailGetByIdResponseDto>(paymentRequestDetail);
            return new SuccessDataResult<PaymentRequestDetailGetByIdResponseDto>(paymentRequestDetailGetByIdResponseDto);
        }

        [SecurityAspect("PaymentRequestDetail.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<PaymentRequestDetailGetListResponseDto>>> GetList()
        {
            var paymentRequestDetail = await _paymentRequestDetailDal.GetList(x => x.IsActive == true);
            var paymentRequestDetailGetListResponseDtos = _mapper.Map<List<PaymentRequestDetailGetListResponseDto>>(paymentRequestDetail);
            paymentRequestDetailGetListResponseDtos = paymentRequestDetailGetListResponseDtos.OrderBy(x => x.ReferenceNumber).ToList();
            return new SuccessDataResult<List<PaymentRequestDetailGetListResponseDto>>(paymentRequestDetailGetListResponseDtos);
        }

        [SecurityAspect("PaymentRequestDetail.GetListByPaymentRequestId", Priority = 2)]
        public async Task<IDataResult<List<PaymentRequestDetailGetListByPaymentRequestIdResponseDto>>> GetListByPaymentRequestId(int paymentRequestId)
        {
            var paymentRequestDetail = await _paymentRequestDetailDal.GetList(x => x.PaymentRequestId==paymentRequestId && x.IsActive == true);
            var paymentRequestDetailGetListByPaymentRequestIdResponseDto = _mapper.Map<List<PaymentRequestDetailGetListByPaymentRequestIdResponseDto>>(paymentRequestDetail);
            paymentRequestDetailGetListByPaymentRequestIdResponseDto = paymentRequestDetailGetListByPaymentRequestIdResponseDto.OrderBy(x => x.ReferenceNumber).ToList();
            return new SuccessDataResult<List<PaymentRequestDetailGetListByPaymentRequestIdResponseDto>>(paymentRequestDetailGetListByPaymentRequestIdResponseDto);
        }

        [SecurityAspect("PaymentRequestDetail.Update", Priority = 2)]
        [ValidationAspect(typeof(PaymentRequestDetailUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IPaymentRequestDetailService.Get", Priority = 4)]
        public async Task<IResult> Update(PaymentRequestDetailUpdateRequestDto paymentRequestDetailUpdateRequestDto)
        {
            var paymentRequestDetail = _mapper.Map<PaymentRequestDetail>(paymentRequestDetailUpdateRequestDto);
            if (paymentRequestDetail != null)
            {
                await _paymentRequestDetailDal.Update(paymentRequestDetail);
                return new SuccessResult(PaymentRequestDetailMessages.Updated);
            }

            return new ErrorResult(PaymentRequestDetailMessages.OperationFailed);
        }
    }
}
