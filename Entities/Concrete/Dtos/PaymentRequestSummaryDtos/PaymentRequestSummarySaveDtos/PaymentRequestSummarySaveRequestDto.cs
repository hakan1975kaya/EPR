using Core.Entities.Abstract;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySaveDtos
{
    public class PaymentRequestSummarySaveRequestDto : IDto
    {
        public List< PaymentRequestSummary> PaymentRequestSummaries { get; set; }
        public SaveTypeEnum SaveType { get; set; }
        public string RequestNumber { get; set; }
        public List<HcpFileRequestDto> HcpFileRequests { get; set; }
    }
}
