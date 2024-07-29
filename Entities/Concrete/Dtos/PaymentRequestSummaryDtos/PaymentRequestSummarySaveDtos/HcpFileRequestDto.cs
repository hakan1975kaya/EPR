using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySaveDtos
{
    public class HcpFileRequestDto : IDto
    {
        public Guid HcpId { get; set; }
        public string Extension { get; set; }
        public string Explanation { get; set; }
    }
}
