using Core.HttpAccessor.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Sftp.Abstract;
using Sftp.Constants.Messages;
using Sftp.Dtos.SftpDownloadFileDtos;
using Sftp.Dtos.SftpUploadFileDtos;

namespace Sftp.Concrete
{
    public class SftpDal : ISftpDal
    {
        private IHttpContextAccessorService _httpContextAccessorService;
        public SftpDal(IHttpContextAccessorService httpContextAccessorService)
        {
            _httpContextAccessorService = httpContextAccessorService;
        }
        public async Task<IDataResult<SftpClient>> CreateSftpClient()
        {
            return new SuccessDataResult<SftpClient>(new SftpClient("10.125.3.81", 22, "tandemsftp", "TpfM201303t6"));
        }
        public async Task<IResult> SftpDownload(SftpDownloadFileRequestDto SftpDownloadFileRequestDto)
        {

            var registrationNumberResult = await _httpContextAccessorService.GetRegistrationNumber();

            if (registrationNumberResult != null)
            {
                if (registrationNumberResult.Success)
                {
                    if (registrationNumberResult.Data != null)
                    {
                        var registrationNumber = registrationNumberResult.Data;

                        if (SftpDownloadFileRequestDto != null)
                        {
                            if (SftpDownloadFileRequestDto.SftpFileName != null)
                            {

                                var filePathLocal = "c:/sftp/" + registrationNumber.ToString() + SftpDownloadFileRequestDto.SftpFileName;

                                using (Stream stream = File.OpenWrite(filePathLocal))
                                {
                                    var _sftpClientDataResult = await CreateSftpClient();

                                    if (_sftpClientDataResult != null)
                                    {
                                        if (_sftpClientDataResult.Success)
                                        {
                                            if (_sftpClientDataResult.Data != null)
                                            {
                                                var _sftpClient = _sftpClientDataResult.Data;

                                                _sftpClient.Connect();

                                                _sftpClient.DownloadFile(SftpDownloadFileRequestDto.SftpFileName, stream);

                                                _sftpClient.Disconnect();
                                                _sftpClient.Dispose();
                                                return new SuccessResult(TandemMessages.Downloaded);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new ErrorResult(TandemMessages.OperationFailed);
        }

        public async Task<IResult> UploadFile(SftpUploadFileRequestDto SftpUploadFileRequestDto)
        {
            var _sftpClientDataResult = await CreateSftpClient();
            if (_sftpClientDataResult != null)
            {
                if (_sftpClientDataResult.Success)
                {
                    if (_sftpClientDataResult.Data != null)
                    {
                        var _sftpClient = _sftpClientDataResult.Data;
                        _sftpClient.Connect();
                        _sftpClient.UploadFile(SftpUploadFileRequestDto.Stream, SftpUploadFileRequestDto.FilePath);
                        _sftpClient.Disconnect();
                        _sftpClient.Dispose();
                        return new SuccessResult(TandemMessages.Uploaded);
                    }
                }
            }
            return new ErrorResult(TandemMessages.OperationFailed);

        }
        public async Task<IResult> DeleteFile(string filePath)
        {
            var _sftpClientDataResult = await CreateSftpClient();
            if (_sftpClientDataResult != null)
            {
                if (_sftpClientDataResult.Success)
                {
                    if (_sftpClientDataResult.Data != null)
                    {
                        var _sftpClient = _sftpClientDataResult.Data;
                        _sftpClient.Connect();
                        _sftpClient.DeleteFile(filePath);
                        _sftpClient.Disconnect();
                        _sftpClient.Dispose();
                        return new SuccessResult(TandemMessages.Deleted);
                    }
                }
            }
            return new ErrorResult(TandemMessages.OperationFailed);
        }
        public async Task<IDataResult<string>> GetDirectory(string prefix)
        {
            var registrationNumberResult = await _httpContextAccessorService.GetRegistrationNumber();

            if (registrationNumberResult != null)
            {
                if (registrationNumberResult.Success)
                {
                    if (registrationNumberResult.Data != null)
                    {
                        var registrationNumber = registrationNumberResult.Data;

                        var sftpDirectory = "/kurum/" + prefix + "/firma";

                        var directory = "c:/sftp/" + registrationNumber.ToString() + sftpDirectory;

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        return new SuccessDataResult<string>(sftpDirectory);
                    }
                }
            }

            return new ErrorDataResult<string>(null);
        }
        public async Task<IDataResult<List<string>>> GetFileNames(string directory)
        {
            List<SftpFile> files = new List<SftpFile>();

            List<string> fullFileNames = new List<string>();

            var _sftpClientDataResult = await CreateSftpClient();
            if (_sftpClientDataResult != null)
            {
                if (_sftpClientDataResult.Success)
                {
                    if (_sftpClientDataResult.Data != null)
                    {
                        var _sftpClient = _sftpClientDataResult.Data;

                        _sftpClient.Connect();

                        _sftpClient.ChangeDirectory(directory);

                        files = _sftpClient.ListDirectory(".").Where(x => x.Name.EndsWith("xls") || x.Name.EndsWith("xlsx")).ToList();

                        if (files != null)
                        {
                            if (files.Count > 0)
                            {
                                foreach (SftpFile file in files)
                                {
                                    fullFileNames.Add(file.FullName);
                                }
                            }
                        }
                        _sftpClient.Disconnect();
                        _sftpClient.Dispose();
                        return new SuccessDataResult<List<string>>(fullFileNames);
                    }
                }
            }
            return new ErrorDataResult<List<string>>(null);
        }



    }
}
