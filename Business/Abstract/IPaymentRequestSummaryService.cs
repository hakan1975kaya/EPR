using Core.Utilities.Results.Abstract;
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

namespace Business.Abstract
{
    public interface IPaymentRequestSummaryService
    {
        Task<IDataResult<PaymentRequestSummaryGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<PaymentRequestSummaryGetListByRequestIdResponseDto>>> GetList();
        Task<IResult> Add(PaymentRequestSummaryAddRequestDto paymentRequestSummaryAddRequestDto);
        Task<IResult> Update(PaymentRequestSummaryUpdateRequestDto paymentRequestSummaryUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<PaymentRequestSummaryGetListByPaymentRequestIdResponseDto>>> GetListByPaymentRequestId(int paymentRequestId);
        Task<IDataResult<List<PaymentRequestSummarySearchResponseDto>>> Search(PaymentRequestSummarySearchRequestDto paymentRequestSummarySearchRequestDto);
        Task<IResult> Save(PaymentRequestSummarySaveRequestDto paymentRequestSummarySaveRequestDto);
        Task<IDataResult<List<PaymentRequestSummaryAmountByCorporateIdYearResponseDto>>> AmountByCorporateIdYear(PaymentRequestSummaryAmountByCorporateIdYearRequestDto paymentRequestSummaryAmountByCorporateIdYearRequestDto);
        Task<IDataResult<List<PaymentRequestSummaryGetByTodayResponseDto>>> GetByToday();
        Task<IDataResult<List<PaymentRequestSummaryTotalOutgoingPaymentResponseDto>>> TotalOutgoingPayment();


    }
}
