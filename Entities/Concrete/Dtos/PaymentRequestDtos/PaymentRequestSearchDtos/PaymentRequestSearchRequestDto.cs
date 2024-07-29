using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos
{
    public class PaymentRequestSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}
