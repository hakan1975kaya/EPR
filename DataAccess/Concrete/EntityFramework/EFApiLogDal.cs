using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFApiLogDal : EFEntityRepositoryBase<ApiLog, EPRLogContext>, IApiLogDal
    {
        public async Task<List<ApiLogSearchResponseDto>> Search(ApiLogSearchRequestDto apiLogSearchRequestDto)
        {
            var filter = apiLogSearchRequestDto.Filter;
            var audit = apiLogSearchRequestDto.Audit;

            using (var context = new EPRLogContext())
            {
                var result = (from al in context.ApiLogs
                              where (al.Detail.ToLower().Contains(filter.ToLower()) ||
                              al.Date.ToString().ToLower().Contains(filter)) &&
                              (al.Audit == audit.ToString() || audit == AuditEnum.None)
                              select new ApiLogSearchResponseDto
                              {
                                  Id = al.Id,
                                  Audit = al.Audit,
                                  Date = al.Date,
                                  Detail = al.Detail,
                              }).Take(100).OrderByDescending(x => x.Date);

                return result.ToList();
            }
        }
    }
}
