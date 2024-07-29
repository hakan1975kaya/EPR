using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
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
    public class EFSftpDownloadDal : EFEntityRepositoryBase<SftpDownload, EPRContext>, ISftpDownloadDal
    {
        public async Task<List<SftpDownloadSearchResponseDto>> Search(SftpDownloadSearchRequestDto sftpDownloadSearchRequestDto)
        {
            var filter = sftpDownloadSearchRequestDto.Filter;

            using (var context = new EPRContext())
            {
                var result = (from s in context.SftpDownloads
                              where s.SftpFileName.ToLower().Contains(filter.ToLower()) &&
                              s.IsActive == true
                              select new SftpDownloadSearchResponseDto
                              {
                                  Id = s.Id,
                                  CorporateId = s.CorporateId,
                                  UserId = s.UserId,
                                  SftpFileName = s.SftpFileName,
                                  Status = s.Status,
                                  IsActive = s.IsActive
                              }).OrderByDescending(x => x.Status);

                return result.ToList();
            }
        }



    }
}
