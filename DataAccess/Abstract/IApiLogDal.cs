using Core.DataAccess.Abstract;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IApiLogDal: IEntityRepository<ApiLog>
    {
        Task<List<ApiLogSearchResponseDto>> Search(ApiLogSearchRequestDto apiLogSearchRequestDto);
    }
}
