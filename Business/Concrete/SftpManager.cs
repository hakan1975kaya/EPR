using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.SftpValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.HttpAccessor.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums;
using Sftp.Abstract;
using Sftp.Dtos.SftpDownloadFileDtos;
using Sftp.Dtos.SftpUploadFileDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class SftpManager : ISftpService
    {
        private ISftpDal _sftpDal;
        private ISftpDownloadDal _sftpDownloadDal;
        private IUserDal _userDal;
        private ICorporateDal _corporateDal;
        private IHttpContextAccessorService _httpContextAccessorService;
        public SftpManager(
            ISftpDal sftpDal,
            ISftpDownloadDal sftpDownloadDal,
            IUserDal userDal,
            ICorporateDal corporateDal,
            IHttpContextAccessorService httpContextAccessorService
            )
        {
            _sftpDal = sftpDal;
            _sftpDownloadDal = sftpDownloadDal;
            _corporateDal = corporateDal;
            _userDal = userDal;
            _httpContextAccessorService = httpContextAccessorService;
        }

        [SecurityAspect("Sftp.SftpDownload", Priority = 2)]
        [ValidationAspect(typeof(SftpDownloadFileRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> SftpDownload(SftpDownloadFileRequestDto SftpDownloadFileRequestDto)
        {
            if (SftpDownloadFileRequestDto != null)
            {
                if (SftpDownloadFileRequestDto.SftpFileName != null)
                {
                    if (SftpDownloadFileRequestDto.Prefix != null)
                    {

                        var result = await _sftpDal.SftpDownload(SftpDownloadFileRequestDto);
                        if (result != null)
                        {
                            if (result.Success)
                            {
                                var sftpFileName = SftpDownloadFileRequestDto.SftpFileName;
                                var prefix = SftpDownloadFileRequestDto.Prefix;

                                var corporate = await _corporateDal.Get(x => x.Prefix == prefix && x.IsActive == true);

                                if (corporate != null)
                                {
                                    var corporateId = corporate.Id;
                                    var registrationNumberResult = await _httpContextAccessorService.GetRegistrationNumber();

                                    if (registrationNumberResult != null)
                                    {
                                        if (registrationNumberResult.Success)
                                        {
                                            if (registrationNumberResult.Data != null)
                                            {
                                                var registrationNumber = registrationNumberResult.Data;

                                                if (registrationNumber > 0)
                                                {
                                                    var user = await _userDal.Get(x => x.RegistrationNumber == registrationNumber && x.IsActive == true);

                                                    if (user != null)
                                                    {
                                                        var userId = user.Id;

                                                        var sftpDownloadDeleteds = await _sftpDownloadDal.GetList(x => x.CorporateId == corporateId && x.SftpFileName == sftpFileName && x.Status == StatusEnum.DownloadedFromSftp && x.IsActive == true);

                                                        if (sftpDownloadDeleteds != null)
                                                        {
                                                            if (sftpDownloadDeleteds.Count() > 0)
                                                            {
                                                                foreach (var sftpDownloadDeleted in sftpDownloadDeleteds)
                                                                {
                                                                    sftpDownloadDeleted.Optime = DateTime.Now;
                                                                    sftpDownloadDeleted.IsActive = false;
                                                                    sftpDownloadDeleted.UserId = userId;
                                                                    await _sftpDownloadDal.Update(sftpDownloadDeleted);
                                                                }
                                                            }
                                                        }

                                                        var sftpDownload = new SftpDownload
                                                        {
                                                            CorporateId = corporateId,
                                                            SftpFileName = sftpFileName,
                                                            IsActive = true,
                                                            Optime = DateTime.Now,
                                                            Status = StatusEnum.DownloadedFromSftp,
                                                            UserId = userId,
                                                        };

                                                        await _sftpDownloadDal.Add(sftpDownload);

                                                        return new SuccessResult(SftpMessages.Added);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return new ErrorResult(SftpMessages.OperationFailed);
        }

        [SecurityAspect("Sftp.UploadFile", Priority = 2)]
        [ValidationAspect(typeof(SftpUploadFileRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> UploadFile(SftpUploadFileRequestDto SftpUploadFileRequestDto)
        {
            return await _sftpDal.UploadFile(SftpUploadFileRequestDto);
        }

        [SecurityAspect("Sftp.DeleteFile", Priority = 2)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 3)]
        [TransactionAspect(Priority = 4)]
        public async Task<IResult> DeleteFile(string filePath)
        {
            return await _sftpDal.DeleteFile(filePath);
        }

        [SecurityAspect("Sftp.GetDirectory", Priority = 2)]
        public async Task<IDataResult<string>> GetDirectory(string prefix)
        {
            return await _sftpDal.GetDirectory(prefix);
        }

        [SecurityAspect("Sftp.GetFileNames", Priority = 2)]
        public async Task<IDataResult<List<string>>> GetFileNames(string directory)
        {
            var fileNames = new List<string>();
            var sftpFileNamesDataResult = await _sftpDal.GetFileNames(directory);
            if (sftpFileNamesDataResult != null)
            {
                if (sftpFileNamesDataResult.Success)
                {
                    if (sftpFileNamesDataResult.Data != null)
                    {
                        if (sftpFileNamesDataResult.Data.Count() > 0)
                        {
                            var sftpFileNames = sftpFileNamesDataResult.Data;

                            if (sftpFileNames != null)
                            {
                                if (sftpFileNames.Count > 0)
                                {
                                    foreach (var sftpFileName in sftpFileNames)
                                    {
                                        var availableFileName = await _sftpDownloadDal.Get(x => x.IsActive == true && x.SftpFileName == sftpFileName && (x.Status == StatusEnum.SummaryApproved || x.Status == StatusEnum.SummaryRejected || x.Status == StatusEnum.SummaryWaitingForApproval));

                                        if (availableFileName == null)
                                        {
                                            fileNames.Add(sftpFileName);
                                        }
                                    }
                                    return new SuccessDataResult<List<string>>(fileNames);
                                }
                            }
                        }
                    }
                }
            }
            return new ErrorDataResult<List<string>>(null);
        }




    }
}
