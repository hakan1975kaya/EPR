using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogAddDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogGetByIdDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogGetListDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogUpdateDtos;

namespace Business.Abstract
{
    public interface IWebLogService
    {
        Task<IDataResult<WebLogGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<WebLogGetListResponseDto>>> GetList();
        Task<IResult> Add(WebLogAddRequestDto WebLogAddRequestDto);
        Task<IResult> Update(WebLogUpdateRequestDto WebLogUpdateRequestDto);
        Task<IDataResult<List<WebLogSearchResponseDto>>> Search(WebLogSearchRequestDto webLogSearchRequestDto);
    }
}
