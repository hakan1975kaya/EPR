using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.HcpUploadValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadAddDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadGetByldDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadGetListDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadSearchDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadUpdateDtos;
using Entities.Concrete.Entities;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class HcpUploadManager : IHcpUploadService
    {
        private IHcpUploadDal _hcpUploadDal;
        private IMapper _mapper;
        public HcpUploadManager(IHcpUploadDal hcpUploadDal, IMapper mapper)
        {
            _hcpUploadDal = hcpUploadDal;
            _mapper = mapper;

        }

        [SecurityAspect("HcpUpload.Add", Priority = 2)]
        [ValidationAspect(typeof(HcpUploadAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IHcpUploadService.Get", Priority = 4)]
        public async Task<IResult> Add(HcpUploadAddRequestDto hcpUploadAddRequestDto)
        {
            var hcpUploadAdd = _mapper.Map<HcpUpload>(hcpUploadAddRequestDto);
            await _hcpUploadDal.Add(hcpUploadAdd);
            return new SuccessResult(HcpUploadMessages.Added);
        }

        [SecurityAspect("HcpUpload.GetById", Priority = 2)]
        public async Task<IDataResult<HcpUploadGetByIdResponseDto>> GetById(int id)
        {

            var hcpUpload = await _hcpUploadDal.Get(x => x.Id == id && x.IsActive == true);
            var hcpUploadGetByIdResponseDto = _mapper.Map<HcpUploadGetByIdResponseDto>(hcpUpload);
            return new SuccessDataResult<HcpUploadGetByIdResponseDto>(hcpUploadGetByIdResponseDto);
        }

        [SecurityAspect("HcpUpload.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<HcpUploadGetListResponseDto>>> GetList()
        {
            var hcpUploads = await _hcpUploadDal.GetList(x=>x.IsActive==true);
            var hcpUploadGetListResponseDtos = _mapper.Map<List<HcpUploadGetListResponseDto>>(hcpUploads);
            return new SuccessDataResult<List<HcpUploadGetListResponseDto>>(hcpUploadGetListResponseDtos);
        }

        [SecurityAspect("HcpUpload.Search", Priority = 2)]
        public async Task<IDataResult<List<HcpUploadSearchResponseDto>>> Search(HcpUploadSearchRequestDto hcpUploadSearchRequestDto)
        {
            return new SuccessDataResult<List<HcpUploadSearchResponseDto>>(await _hcpUploadDal.Search(hcpUploadSearchRequestDto));
        }

        [SecurityAspect("HcpUpload.Update", Priority = 2)]
        [ValidationAspect(typeof(HcpUploadUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IHcpUploadService.Get", Priority = 4)]
        public async Task<IResult> Update(HcpUploadUpdateRequestDto hcpUploadUpdateRequestDto)
        {
            var hcpUpload = _mapper.Map<HcpUpload>(hcpUploadUpdateRequestDto);
            if (hcpUpload != null)
            {
                await _hcpUploadDal.Update(hcpUpload);
                return new SuccessResult(HcpUploadMessages.Updated);
            }

            return new ErrorResult(HcpUploadMessages.OperationFailed);

        }
    }
}
