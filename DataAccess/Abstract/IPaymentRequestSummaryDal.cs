using Core.DataAccess.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAmountByCorporateIdYearDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryChartByCorporateIdYear;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySearchDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestTotalOutgoingPaymentDtos;
using Entities.Concrete.Entities;

namespace DataAccess.Abstract
{
    public interface IPaymentRequestSummaryDal: IEntityRepository<PaymentRequestSummary>
    {
        Task<List<PaymentRequestSummarySearchResponseDto>> Search(PaymentRequestSummarySearchRequestDto paymentRequestSummarySearchRequestDto);
        Task<List<PaymentRequestSummaryAmountByCorporateIdYearResponseDto>> AmountByCorporateIdYear(PaymentRequestSummaryAmountByCorporateIdYearRequestDto paymentRequestSummaryAmountByCorporateIdYearRequestDto);
        Task<List<PaymentRequestSummaryGetByTodayResponseDto>> GetByToday();
        Task<List<PaymentRequestSummaryTotalOutgoingPaymentResponseDto>> TotalOutgoingPayment();
    }
}
