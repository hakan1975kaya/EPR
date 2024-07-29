using Core.Utilities.Results.Abstract;
using Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos;

namespace Business.Abstract
{
    public interface ITandemService
    {
        Task<IDataResult<TandemPaymentTransferResponseExternalDto>> PaymentTransfer(TandemPaymentTransferRequestExternalDto tandemPaymentTransferRequestExternalDto);
        Task<IDataResult<TandemPaymentUpdateResponseExternalDto>> PaymentUpdate(TandemPaymentUpdateRequestExternalDto tandemPaymentUpdateRequestExternalDto);
        Task<IDataResult<TandemPaymentInquiryResponseExternalDto>> PaymentInquiry(TandemPaymentInquiryRequestExternalDto tandemPaymentInquiryRequestExternalDto);
        Task<IDataResult<TandemCorporateDefineResponseExternalDto>> CorporateDefine(TandemCorporateDefineRequestExternalDto tandemCorporateDefineRequestExternalDto);
    }
}
