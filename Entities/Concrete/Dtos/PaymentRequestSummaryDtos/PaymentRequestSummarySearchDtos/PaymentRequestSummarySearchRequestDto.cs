using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySearchDtos
{
    public class PaymentRequestSummarySearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}
