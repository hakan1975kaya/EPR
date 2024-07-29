using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetByldDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetListDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadUpdateDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetBySftpFileNameDtos;

namespace Business.Abstract
{
    public interface ISftpDownloadService
    {
        Task<IDataResult<SftpDownloadGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<SftpDownloadGetBySftpFileNameResponseDto>> GetBySftpFileName(string sftpFileName);
        Task<IDataResult<List<SftpDownloadGetListResponseDto>>> GetList();
        Task<IResult> Add(SftpDownloadAddRequestDto sftpDownloadAddRequestDto);
        Task<IResult> Update(SftpDownloadUpdateRequestDto sftpDownloadUpdateRequestDto);
        Task<IDataResult<List<SftpDownloadSearchResponseDto>>> Search(SftpDownloadSearchRequestDto sftpDownloadSearchRequestDto);
        Task<IResult> OpenToPaymentRequest(int PaymentRequestId);

    }
}
