using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.CorporateValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.CorporateDtos.CorporateAddDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetByIdDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateUpdateDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSaveDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListPrefixAvailableDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetByCorporateCodeDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class CorporateManager : ICorporateService
    {
        private ICorporateDal _corporateDal;
        private IMapper _mapper;
        public CorporateManager(ICorporateDal corporateDal, IMapper mapper)
        {
            _corporateDal = corporateDal;
            _mapper = mapper;

        }

        [SecurityAspect("Corporate.Add", Priority = 2)]
        [ValidationAspect(typeof(CorporateAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ICorporateService.Get", Priority = 4)]
        public async Task<IResult> Add(CorporateAddRequestDto corporateAddRequestDto)
        {
            var corporateAdd = _mapper.Map<Corporate>(corporateAddRequestDto);
            await _corporateDal.Add(corporateAdd);
            return new SuccessResult(CorporateMessages.Added);
            ;
        }

        [SecurityAspect("Corporate.Delete", Priority = 2)]
        [CacheRemoveAspect("ICorporateService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var corporate = await _corporateDal.Get(x => x.Id == id && x.IsActive == true);
            if (corporate != null)
            {
                corporate.IsActive = false;
                await _corporateDal.Update(corporate);
                return new SuccessResult(CorporateMessages.Deleted);

            }
            return new ErrorResult(CorporateMessages.OperationFailed);

        }

        [SecurityAspect("Corporate.GetById", Priority = 2)]
        public async Task<IDataResult<CorporateGetByIdResponseDto>> GetById(int id)
        {

            var corporate = await _corporateDal.Get(x => x.Id == id && x.IsActive == true);
            var corporateGetByIdResponseDto = _mapper.Map<CorporateGetByIdResponseDto>(corporate);
            return new SuccessDataResult<CorporateGetByIdResponseDto>(corporateGetByIdResponseDto);
        }

        [SecurityAspect("Corporate.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<CorporateGetListResponseDto>>> GetList()
        {
            var corporates = await _corporateDal.GetList(x => x.IsActive == true);
            var corporateGetListResponseDtos = _mapper.Map<List<CorporateGetListResponseDto>>(corporates);
            corporateGetListResponseDtos = corporateGetListResponseDtos.OrderBy(x => x.CorporateCode).ToList();
            return new SuccessDataResult<List<CorporateGetListResponseDto>>(corporateGetListResponseDtos);
        }

        [SecurityAspect("Corporate.Update", Priority = 2)]
        [ValidationAspect(typeof(CorporateUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ICorporateService.Get", Priority = 4)]
        public async Task<IResult> Update(CorporateUpdateRequestDto corporateUpdateRequestDto)
        {
            var corporate = _mapper.Map<Corporate>(corporateUpdateRequestDto);
            if (corporate != null)
            {
                await _corporateDal.Update(corporate);
                return new SuccessResult(CorporateMessages.Updated);
            }

            return new ErrorResult(CorporateMessages.OperationFailed);

        }

        [SecurityAspect("Corporate.Save", Priority = 2)]
        [ValidationAspect(typeof(CorporateAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ICorporateService.Get", Priority = 4)]
        public async Task<IResult> Save(CorporateSaveRequestDto corporateSaveRequestDto)
        {
            var saveType = corporateSaveRequestDto.SaveType;
            var corporate = corporateSaveRequestDto.Corporate;
            if (saveType == SaveTypeEnum.Add)
            {
                corporate.IsActive = true;
                corporate.Optime = DateTime.Now;
                await _corporateDal.Add(corporate);

                return new SuccessResult(CorporateMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                corporate.IsActive = true;
                corporate.Optime = DateTime.Now;
                await _corporateDal.Update(corporate);

                return new SuccessResult(CorporateMessages.Updated);
            }

            return new ErrorResult(CorporateMessages.OperationFailed);
        }

        [SecurityAspect("Corporate.Search", Priority = 2)]
        [ValidationAspect(typeof(CorporateSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<CorporateSearchResponseDto>>> Search(CorporateSearchRequestDto corporateSearchRequestDto)
        {
            var filter = corporateSearchRequestDto.Filter;
            var corporates = await _corporateDal.GetList(x => (x.CorporateCode.ToString().ToLower().Contains(filter.ToLower()) || x.CorporateName.ToLower().Contains(filter.ToLower())) && x.IsActive == true);
            var corporateSearchResponseDtos = _mapper.Map<List<CorporateSearchResponseDto>>(corporates.OrderBy(x => x.CorporateCode));
            return new SuccessDataResult<List<CorporateSearchResponseDto>>(corporateSearchResponseDtos);
        }

        [SecurityAspect("Corporate.GetListPrefixAvailable", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<CorporateGetListPrefixAvailableResponseDto>>> GetListPrefixAvailable()
        {
            var corporates = await _corporateDal.GetList(x => !string.IsNullOrEmpty(x.Prefix) && x.IsActive == true);
            var corporateGetListPrefixAvailableResponseDtos = _mapper.Map<List<CorporateGetListPrefixAvailableResponseDto>>(corporates);
            corporateGetListPrefixAvailableResponseDtos = corporateGetListPrefixAvailableResponseDtos.OrderBy(x => x.CorporateCode).ToList();
            return new SuccessDataResult<List<CorporateGetListPrefixAvailableResponseDto>>(corporateGetListPrefixAvailableResponseDtos);
        }

        [SecurityAspect("Corporate.GetByCorporateCode", Priority = 2)]
        public async Task<IDataResult<CorporateGetByCorporateCodeResponseDto>> GetByCorporateCode(int corporateCode)
        {
            var corporate = await _corporateDal.Get(x => x.CorporateCode == corporateCode && x.IsActive == true);
            var corporateGetByCorporateCodeResponseDto = _mapper.Map<CorporateGetByCorporateCodeResponseDto>(corporate);
            return new SuccessDataResult<CorporateGetByCorporateCodeResponseDto>(corporateGetByCorporateCodeResponseDto);
        }

    }
}
