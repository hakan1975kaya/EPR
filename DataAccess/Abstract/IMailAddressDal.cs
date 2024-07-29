using Core.DataAccess.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;

namespace DataAccess.Abstract
{
    public interface IMailAddressDal : IEntityRepository<MailAddress>
    {
        Task<List<MailAddressSearchResponseDto>> Search(MailAddressSearchRequestDto mailAddressSearchRequestDto);
    }
}
