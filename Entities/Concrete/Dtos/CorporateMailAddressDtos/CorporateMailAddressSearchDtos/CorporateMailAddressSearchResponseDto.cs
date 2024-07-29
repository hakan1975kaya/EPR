using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos
{
    public class CorporateMailAddressSearchResponseDto : IDto
    {
        public int Id { get; set; }
        public int CorporateId { get; set; }
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public int MailAddressId { get; set; }
        public string Address { get; set; }
        public bool IsCC { get; set; }
        public bool IsPtt { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
