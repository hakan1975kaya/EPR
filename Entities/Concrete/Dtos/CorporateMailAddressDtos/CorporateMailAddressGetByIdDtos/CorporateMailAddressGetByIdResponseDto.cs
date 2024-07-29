using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetByIdDtos
{
    public class CorporateMailAddressGetByIdResponseDto : IDto
    {
        public int Id { get; set; }
        public int CorporateId { get; set; }
        public int MailAddressId { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
