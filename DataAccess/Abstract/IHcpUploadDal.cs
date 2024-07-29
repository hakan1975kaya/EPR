using Core.DataAccess.Abstract;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadSearchDtos;
using Entities.Concrete.Entities;

namespace DataAccess.Abstract
{
    public interface IHcpUploadDal:IEntityRepository<HcpUpload>
    {
        Task<List<HcpUploadSearchResponseDto>> Search(HcpUploadSearchRequestDto hcpUploadSearchRequestDto);
    }
}
