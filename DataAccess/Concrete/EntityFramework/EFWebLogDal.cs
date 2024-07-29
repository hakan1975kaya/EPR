using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListGetByUserIdResponseDtos;
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
    public class EFWebLogDal : EFEntityRepositoryBase<WebLog, EPRLogContext>, IWebLogDal
    {
        public async Task<List<WebLogSearchResponseDto>> Search(WebLogSearchRequestDto webLogSearchRequestDto)
        {
            var filter = webLogSearchRequestDto.Filter;
            var audit = webLogSearchRequestDto.Audit;

            using (var context = new EPRLogContext())
            {
                var result = (from wl in context.WebLogs
                              where (wl.Detail.ToLower().Contains(filter.ToLower()) ||
                              wl.Date.ToString().ToLower().Contains(filter)) &&
                              (wl.Audit == audit ||
                              audit == AuditEnum.None)
                              select new WebLogSearchResponseDto
                              {
                                  Id = wl.Id,
                                  Audit = wl.Audit,
                                  Date = wl.Date,
                                  Detail = wl.Detail,
                              }).Take(100).OrderByDescending(x => x.Date);

                return result.ToList();
            }
        }
    }
}
