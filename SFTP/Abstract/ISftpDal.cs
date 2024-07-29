using Core.Utilities.Results.Abstract;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Sftp.Dtos.SftpDownloadFileDtos;
using Sftp.Dtos.SftpUploadFileDtos;

namespace Sftp.Abstract
{
    public interface ISftpDal
    {
        Task<IResult> SftpDownload(SftpDownloadFileRequestDto SftpDownloadFileRequestDto);
        Task<IResult> UploadFile(SftpUploadFileRequestDto SftpUploadFileRequestDto);
        Task<IResult> DeleteFile(string filePath);
        Task<IDataResult<string>> GetDirectory(string Prefix);
        Task<IDataResult<List<string>>> GetFileNames(string directory);
        Task<IDataResult<SftpClient>>  CreateSftpClient();
    }
}
