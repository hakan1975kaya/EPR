using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos
{
    public class MailAddressSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}
