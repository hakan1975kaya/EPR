using Core.Utilities.Results.Abstract;
using Sftp.Dtos.SftpDownloadFileDtos;
using Sftp.Dtos.SftpUploadFileDtos;

namespace Business.Abstract
{
    public interface ISftpService
    {
        Task<IResult> SftpDownload(SftpDownloadFileRequestDto SftpDownloadFileRequestDto);
        Task<IResult> UploadFile(SftpUploadFileRequestDto SftpUploadFileRequestDto );
        Task<IResult> DeleteFile(string filePath);
        Task<IDataResult<string>> GetDirectory(string prefix);
        Task<IDataResult<List<string>>> GetFileNames(string directory);
    }
}
