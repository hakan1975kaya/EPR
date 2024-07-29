using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySearchDtos
{
    public class PaymentRequestSummarySearchResponseDto : IDto
    {
        public int Id { get; set; }
        public int PaymentRequestId { get; set; }
        public string RequestNumber { get; set; }
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime UploadDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime SystemEnteredDate { get; set; }
        public TimeSpan SystemEnteredTime { get; set; }
        public int SystemEnteredRegistrationNumber { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
