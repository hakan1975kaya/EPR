using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadAddDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadGetByldDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadGetListDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadSearchDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadUpdateDtos;

namespace Business.Abstract
{
    public interface IHcpUploadService
    {
        Task<IDataResult<HcpUploadGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<HcpUploadGetListResponseDto>>> GetList();
        Task<IResult> Add(HcpUploadAddRequestDto hcpUploadAddRequestDto);
        Task<IResult> Update(HcpUploadUpdateRequestDto hcpUploadUpdateRequestDto);
        Task<IDataResult<List<HcpUploadSearchResponseDto>>> Search(HcpUploadSearchRequestDto hcpUploadSearchRequestDto);
    }
}
