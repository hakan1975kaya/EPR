using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.MenuValidators;
using Business.ValidationRules.FluentValidation.WebLogValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.WebLogDtos.WebLogAddDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogGetByIdDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogGetListDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogUpdateDtos;
using Entities.Concrete.Entities;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class WebLogManager : IWebLogService
    {
        private IWebLogDal _webLogDal;
        private IMapper _mapper;
        public WebLogManager(IWebLogDal webLogDal, IMapper mapper)
        {
            _webLogDal = webLogDal;
            _mapper = mapper;

        }

        [ValidationAspect(typeof(WebLogAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IWebLogService.Get", Priority = 4)]
        public async Task<IResult> Add(WebLogAddRequestDto webLogAddRequestDto)
        {
            var webLogAdd = _mapper.Map<WebLog>(webLogAddRequestDto);
            await _webLogDal.Add(webLogAdd);
            return new SuccessResult(WebLogMessages.Added);
        }

        [SecurityAspect("WebLog.GetById", Priority = 2)]
        public async Task<IDataResult<WebLogGetByIdResponseDto>> GetById(int id)
        {

            var webLog = await _webLogDal.Get(x => x.Id == id);
            var webLogGetByIdResponseDto = _mapper.Map<WebLogGetByIdResponseDto>(webLog);
            return new SuccessDataResult<WebLogGetByIdResponseDto>(webLogGetByIdResponseDto);
        }

        [SecurityAspect("WebLog.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<WebLogGetListResponseDto>>> GetList()
        {
            var webLogs = await _webLogDal.GetList();
            var webLogGetListResponseDtos = _mapper.Map<List<WebLogGetListResponseDto>>(webLogs);
            return new SuccessDataResult<List<WebLogGetListResponseDto>>(webLogGetListResponseDtos);
        }

        [SecurityAspect("WebLog.Search", Priority = 2)]
        [ValidationAspect(typeof(WebLogSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<WebLogSearchResponseDto>>> Search(WebLogSearchRequestDto webLogSearchRequestDto)
        {
            return new SuccessDataResult<List<WebLogSearchResponseDto>>( await _webLogDal.Search(webLogSearchRequestDto));
        }

        [SecurityAspect("WebLog.Update", Priority = 2)]
        [ValidationAspect(typeof(WebLogUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IWebLogService.Get", Priority = 4)]
        public async Task<IResult> Update(WebLogUpdateRequestDto webLogUpdateRequestDto)
        {
            var webLog = _mapper.Map<WebLog>(webLogUpdateRequestDto);
            if (webLog != null)
            {
                await _webLogDal.Update(webLog);
                return new SuccessResult(WebLogMessages.Updated);
            }

            return new ErrorResult(WebLogMessages.OperationFailed);

        }
    }
}
