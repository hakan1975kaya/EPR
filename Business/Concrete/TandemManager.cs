using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.ValidationRules.FluentValidation.TandemValidators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Tandem.Abstract;
using Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class TandemManager : ITandemService
    {
        private ITandemDal _tandemDal;
        private IMapper _mapper;

        public TandemManager(ITandemDal tandemDal,  IMapper mapper )
        {
            _mapper = mapper;
            _tandemDal = tandemDal;
        }

        [SecurityAspect("Tandem.PaymentTransfer", Priority = 2)]
        [ValidationAspect(typeof(TandemPaymentTransferRequestExternalDtoValidator), Priority = 3)]
        public async Task<IDataResult<TandemPaymentTransferResponseExternalDto>> PaymentTransfer(TandemPaymentTransferRequestExternalDto tandemPaymentTransferRequestExternalDto)
        {
            return new SuccessDataResult<TandemPaymentTransferResponseExternalDto>(await _tandemDal.PaymentTransfer(tandemPaymentTransferRequestExternalDto));
        }

        [SecurityAspect("Tandem.PaymentUpdate", Priority = 2)]
        [ValidationAspect(typeof(TandemPaymentUpdateRequestExternalDtoValidator), Priority = 3)]
        public async Task<IDataResult<TandemPaymentUpdateResponseExternalDto>> PaymentUpdate(TandemPaymentUpdateRequestExternalDto tandemPaymentUpdateRequestExternalDto)
        {
            return new SuccessDataResult<TandemPaymentUpdateResponseExternalDto>(await _tandemDal.PaymentUpdate(tandemPaymentUpdateRequestExternalDto));
        }

        [SecurityAspect("Tandem.PaymentInquiry", Priority = 2)]
        [ValidationAspect(typeof(TandemPaymentInquiryRequestExternalDtoValidator), Priority = 3)]
        public async Task<IDataResult<TandemPaymentInquiryResponseExternalDto>> PaymentInquiry(TandemPaymentInquiryRequestExternalDto tandemPaymentInquiryRequestExternalDto)
        {
            return new SuccessDataResult<TandemPaymentInquiryResponseExternalDto>(await _tandemDal.PaymentInquiry(tandemPaymentInquiryRequestExternalDto));
        }

        [SecurityAspect("Tandem.CorporateDefine", Priority = 2)]
        [ValidationAspect(typeof(TandemCorporateDefineRequestExternalDtoValidator), Priority = 3)]
        public async Task<IDataResult<TandemCorporateDefineResponseExternalDto>> CorporateDefine(TandemCorporateDefineRequestExternalDto tandemCorporateDefineRequestExternalDto)
        {
            return new SuccessDataResult<TandemCorporateDefineResponseExternalDto>(await _tandemDal.CorporateDefine(tandemCorporateDefineRequestExternalDto));
        }
    }
}
