using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos
{
    public class PaymentRequestDownloadResponseDto : IDto
    {
        public PaymentRequest PaymentRequest { get; set; }
        public List<PaymentRequestDetail> PaymentRequestDetails { get; set; }
    }
}
