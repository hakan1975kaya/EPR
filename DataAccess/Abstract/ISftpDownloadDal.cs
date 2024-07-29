﻿using Core.DataAccess.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISftpDownloadDal: IEntityRepository<SftpDownload>
    {
        Task<List<SftpDownloadSearchResponseDto>> Search(SftpDownloadSearchRequestDto sftpDownloadSearchRequestDto);
    }
}
