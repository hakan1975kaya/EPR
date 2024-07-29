using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestTotalOutgoingPaymentDtos
{
    public class PaymentRequestSummaryTotalOutgoingPaymentResponseDto : IDto
    {
        public StatusEnum Name { get; set; }
        public decimal Value { get; set; }
    }
}
