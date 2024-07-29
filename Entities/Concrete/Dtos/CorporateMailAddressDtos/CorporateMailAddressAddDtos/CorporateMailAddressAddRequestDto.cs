using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressAddDtos
{
    public class CorporateMailAddressAddRequestDto : IDto
    {
        public int Id { get; set; }
        public int CorporateId { get; set; }
        public int MailAddressId { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
