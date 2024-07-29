using Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos;

namespace Tandem.Abstract
{
    public interface ITandemDal
    {
        Task<TandemPaymentInquiryResponseExternalDto> PaymentInquiry(TandemPaymentInquiryRequestExternalDto tandemPaymentInquiryRequestExternalDto);
        Task<TandemPaymentTransferResponseExternalDto> PaymentTransfer(TandemPaymentTransferRequestExternalDto tandemPaymentTransferRequestExternalDto);
        Task<TandemPaymentUpdateResponseExternalDto> PaymentUpdate(TandemPaymentUpdateRequestExternalDto tandemPaymentUpdateRequestExternalDto);
        Task<TandemCorporateDefineResponseExternalDto> CorporateDefine(TandemCorporateDefineRequestExternalDto tandemCorporateDefineRequestExternalDto);
    }
}
