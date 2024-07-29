using Core.Aspects.Autofac.Performance.Dtos;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetByIdDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetListDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressUpdateDtos;

namespace Business.Abstract
{
    public interface IMailAddressService
    {
        Task<IDataResult<MailAddressGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<MailAddressGetListResponseDto>>> GetList();
        Task<IResult> Add(MailAddressAddRequestDto corporateMailAddressAddRequestDto);
        Task<IResult> Update(MailAddressUpdateRequestDto corporateMailAddressUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<MailAddressSearchResponseDto>>> Search(MailAddressSearchRequestDto corporateMailAddressSearchRequestDto);
        Task<IResult> Save(MailAddressSaveRequestDto corporateMailAddressSaveRequestDto);
        Task<IDataResult<List<MailAddressGetListPttResposeDto>>> GetListPtt();
        Task<IDataResult<List<MailAddressGetListNotPttResposeDto>>> GetListNotPtt();
    }
}
