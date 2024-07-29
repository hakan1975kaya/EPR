using Core.DataAccess.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos;

namespace DataAccess.Abstract
{
    public interface ICorporateMailAddressDal : IEntityRepository<CorporateMailAddress>
    {
        Task<List<CorporateMailAddressSearchResponseDto>> Search(CorporateMailAddressSearchRequestDto corporateMailAddressSearchRequestDto);
        Task<List<CorporateMailAddressGetListByCorporateIdResponseDto>> GetByCorporateId(int CorporateId);
    }
}
