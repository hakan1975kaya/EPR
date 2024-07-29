using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos
{
    public class CorporateMailAddressSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}
