using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.SftpDownloadValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetByldDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetBySftpFileNameDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetListDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadUpdateDtos;
using Entities.Concrete.Entities;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class SftpDownloadManager : ISftpDownloadService
    {
        private ISftpDownloadDal _sftpDownloadDal;
        private IPaymentRequestDal _paymentRequestDal;
        private IMapper _mapper;
        public SftpDownloadManager(ISftpDownloadDal sftpDownloadDal, IPaymentRequestDal paymentRequestDal, IMapper mapper)
        {
            _sftpDownloadDal = sftpDownloadDal;
            _paymentRequestDal = paymentRequestDal;
            _mapper = mapper;

        }

        [SecurityAspect("SftpDownload.Add", Priority = 2)]
        [ValidationAspect(typeof(SftpDownloadAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ISftpDownloadService.Get", Priority = 4)]
        public async Task<IResult> Add(SftpDownloadAddRequestDto sftpDownloadAddRequestDto)
        {
            var sftpDownloadAdd = _mapper.Map<SftpDownload>(sftpDownloadAddRequestDto);
            await _sftpDownloadDal.Add(sftpDownloadAdd);
            return new SuccessResult(SftpDownloadMessages.Added);
        }

        [SecurityAspect("SftpDownload.GetById", Priority = 2)]
        public async Task<IDataResult<SftpDownloadGetByIdResponseDto>> GetById(int id)
        {

            var sftpDownload = await _sftpDownloadDal.Get(x => x.Id == id && x.IsActive == true);
            var sftpDownloadGetByIdResponseDto = _mapper.Map<SftpDownloadGetByIdResponseDto>(sftpDownload);
            return new SuccessDataResult<SftpDownloadGetByIdResponseDto>(sftpDownloadGetByIdResponseDto);
        }

        [SecurityAspect("SftpDownload.GetBySftpFileName", Priority = 2)]
        public async Task<IDataResult<SftpDownloadGetBySftpFileNameResponseDto>> GetBySftpFileName(string sftpFileName)
        {

            var sftpDownload = await _sftpDownloadDal.Get(x => x.SftpFileName == sftpFileName && x.IsActive == true);
            var sftpDownloadGetBySftpFileNameResponseDto = _mapper.Map<SftpDownloadGetBySftpFileNameResponseDto>(sftpDownload);
            return new SuccessDataResult<SftpDownloadGetBySftpFileNameResponseDto>(sftpDownloadGetBySftpFileNameResponseDto);
        }


        [SecurityAspect("SftpDownload.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<SftpDownloadGetListResponseDto>>> GetList()
        {
            var sftpDownloads = await _sftpDownloadDal.GetList(x => x.IsActive == true);
            var sftpDownloadGetListResponseDtos = _mapper.Map<List<SftpDownloadGetListResponseDto>>(sftpDownloads);
            return new SuccessDataResult<List<SftpDownloadGetListResponseDto>>(sftpDownloadGetListResponseDtos);
        }

        [SecurityAspect("SftpDownload.OpenToPaymentRequest", Priority = 2)]
        [CacheRemoveAspect("ISftpDownloadService.Get", Priority = 4)]
        public async Task<IResult> OpenToPaymentRequest(int PaymentRequestId)
        {
            var paymentRequest = await _paymentRequestDal.Get(x => x.Id == PaymentRequestId && x.IsActive == true);
            if (paymentRequest != null)
            {
                var sftpDownload = await _sftpDownloadDal.Get(x => x.Id == paymentRequest.FileId && x.IsActive == true);
                if (sftpDownload != null)
                {
                    sftpDownload.IsActive = false;
                    await _sftpDownloadDal.Update(sftpDownload);
                    return new SuccessResult(SftpDownloadMessages.Updated);
                }
            }
            return new ErrorResult(SftpDownloadMessages.OperationFailed);
        }

        [SecurityAspect("SftpDownload.Search", Priority = 2)]
        [ValidationAspect(typeof(SftpDownloadSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<SftpDownloadSearchResponseDto>>> Search(SftpDownloadSearchRequestDto sftpDownloadSearchRequestDto)
        {
            return new SuccessDataResult<List<SftpDownloadSearchResponseDto>>(await _sftpDownloadDal.Search(sftpDownloadSearchRequestDto));
        }

        [SecurityAspect("SftpDownload.Update", Priority = 2)]
        [ValidationAspect(typeof(SftpDownloadUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ISftpDownloadService.Get", Priority = 4)]
        public async Task<IResult> Update(SftpDownloadUpdateRequestDto sftpDownloadUpdateRequestDto)
        {
            var sftpDownload = _mapper.Map<SftpDownload>(sftpDownloadUpdateRequestDto);
            if (sftpDownload != null)
            {
                await _sftpDownloadDal.Update(sftpDownload);
                return new SuccessResult(SftpDownloadMessages.Updated);
            }

            return new ErrorResult(SftpDownloadMessages.OperationFailed);

        }
    }
}
