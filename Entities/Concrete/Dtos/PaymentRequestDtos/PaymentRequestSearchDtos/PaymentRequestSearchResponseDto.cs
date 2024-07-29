using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos
{
    public class PaymentRequestSearchResponseDto : IDto
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; }
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public int RegistrationNumber { get; set; }
        public MoneyTypeEnum MoneyType { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
