using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressAddDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetByIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSaveDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressUpdateDtos;

namespace Business.Abstract
{
    public interface ICorporateMailAddressService
    {
        Task<IDataResult<CorporateMailAddressGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<CorporateMailAddressGetListResponseDto>>> GetList();
        Task<IResult> Add(CorporateMailAddressAddRequestDto corporateMailAddressAddRequestDto);
        Task<IResult> Update(CorporateMailAddressUpdateRequestDto corporateMailAddressUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<CorporateMailAddressSearchResponseDto>>> Search(CorporateMailAddressSearchRequestDto corporateMailAddressSearchRequestDto);
        Task<IResult> Save(CorporateMailAddressSaveRequestDto corporateMailAddressSaveRequestDto);
        Task<IDataResult<List<CorporateMailAddressGetListByCorporateIdResponseDto>>> GetByCorporateId(int CorporateId);
    }
}
