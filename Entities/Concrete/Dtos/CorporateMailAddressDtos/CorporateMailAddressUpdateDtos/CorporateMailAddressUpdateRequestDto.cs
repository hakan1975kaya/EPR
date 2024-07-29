using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressUpdateDtos
{
    public class CorporateMailAddressUpdateRequestDto : IDto
    {
        public int Id { get; set; }
        public int CorporateId { get; set; }
        public int MailAddressId { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
