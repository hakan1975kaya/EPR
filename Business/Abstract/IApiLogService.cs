using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogAddDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogGetByldDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogGetListDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogUpdateDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IApiLogService
    {
        Task<IDataResult<ApiLogGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<ApiLogGetListResponseDto>>> GetList();
        Task<IResult> Add(ApiLogAddRequestDto apiLogAddRequestDto);
        Task<IResult> Update(ApiLogUpdateRequestDto apiLogUpdateRequestDto);
        Task<IDataResult<List<ApiLogSearchResponseDto>>> Search(ApiLogSearchRequestDto apiLogSearchRequestDto);

    }
}
