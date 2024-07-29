using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.ApiLogValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogAddDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogGetByldDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogGetListDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogUpdateDtos;
using Entities.Concrete.Entities;

namespace Business.Concrete
{
    public class ApiLogManager : IApiLogService
    {
        private IApiLogDal _apiLogDal;
        private IMapper _mapper;
        public ApiLogManager(IApiLogDal apiLogDal, IMapper mapper)
        {
            _apiLogDal = apiLogDal;
            _mapper = mapper;

        }

        [ValidationAspect(typeof(ApiLogAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IApiLogService.Get", Priority = 4)]
        public async Task<IResult> Add(ApiLogAddRequestDto apiLogAddRequestDto)
        {
            var apiLogAdd = _mapper.Map<ApiLog>(apiLogAddRequestDto);
            await _apiLogDal.Add(apiLogAdd);
            return new SuccessResult(ApiLogMessages.Added);
            ;
        }

        [SecurityAspect("ApiLog.GetById", Priority = 2)]
        public async Task<IDataResult<ApiLogGetByIdResponseDto>> GetById(int id)
        {

            var apiLog = await _apiLogDal.Get(x => x.Id == id);
            var apiLogGetByIdResponseDto = _mapper.Map<ApiLogGetByIdResponseDto>(apiLog);
            return new SuccessDataResult<ApiLogGetByIdResponseDto>(apiLogGetByIdResponseDto);
        }

        [SecurityAspect("ApiLog.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<ApiLogGetListResponseDto>>> GetList()
        {
            var apiLogs = await _apiLogDal.GetList();
            var apiLogGetListResponseDtos = _mapper.Map<List<ApiLogGetListResponseDto>>(apiLogs);
            return new SuccessDataResult<List<ApiLogGetListResponseDto>>(apiLogGetListResponseDtos);
        }

        [SecurityAspect("ApiLog.Search", Priority = 2)]
        [ValidationAspect(typeof(ApiLogSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<ApiLogSearchResponseDto>>> Search(ApiLogSearchRequestDto apiLogSearchRequestDto)
        {
            return new SuccessDataResult<List<ApiLogSearchResponseDto>>(await _apiLogDal.Search(apiLogSearchRequestDto));
        }

        [SecurityAspect("ApiLog.Update", Priority = 2)]
        [ValidationAspect(typeof(ApiLogUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IApiLogService.Get", Priority = 4)]
        public async Task<IResult> Update(ApiLogUpdateRequestDto apiLogUpdateRequestDto)
        {
            var apiLog = _mapper.Map<ApiLog>(apiLogUpdateRequestDto);
            if (apiLog != null)
            {
                await _apiLogDal.Update(apiLog);
                return new SuccessResult(ApiLogMessages.Updated);
            }

            return new ErrorResult(ApiLogMessages.OperationFailed);

        }
    }
}
