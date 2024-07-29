using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListByPaymentRequestIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailUpdateDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestGetByIdDtos;

namespace Business.Abstract
{
    public interface IPaymentRequestDetailService
    {
        Task<IDataResult<PaymentRequestDetailGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<PaymentRequestDetailGetListResponseDto>>> GetList();
        Task<IResult> Add(PaymentRequestDetailAddRequestDto paymentRequestDetailAddRequestDto);
        Task<IResult> Update(PaymentRequestDetailUpdateRequestDto paymentRequestDetailUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<PaymentRequestDetailGetListByPaymentRequestIdResponseDto>>> GetListByPaymentRequestId(int paymentRequestId);
    }

}

