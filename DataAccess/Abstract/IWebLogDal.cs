using Core.DataAccess.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IWebLogDal: IEntityRepository<WebLog>
    {
        Task<List<WebLogSearchResponseDto>> Search(WebLogSearchRequestDto webLogSearchRequestDto);
    }
}
