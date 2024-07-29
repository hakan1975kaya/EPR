using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.CorporateMailAddressValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressAddDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetByIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSaveDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressUpdateDtos;
using Entities.Concrete.Enums.GeneralEnums;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class CorporateMailAddressManager : ICorporateMailAddressService
    {
        private ICorporateMailAddressDal _corporateMailAddressDal;
        private IMapper _mapper;
        public CorporateMailAddressManager(ICorporateMailAddressDal corporateMailAddressDal, IMapper mapper)
        {
            _corporateMailAddressDal = corporateMailAddressDal;
            _mapper = mapper;
        }

        [SecurityAspect("CorporateMailAddress.Add", Priority = 2)]
        [ValidationAspect(typeof(CorporateMailAddressAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ICorporateMailAddressService.Get", Priority = 4)]
        public async Task<IResult> Add(CorporateMailAddressAddRequestDto corporateMailAddressAddRequestDto)
        {
            var corporateMailAddress = _mapper.Map<CorporateMailAddress>(corporateMailAddressAddRequestDto);
            await _corporateMailAddressDal.Add(corporateMailAddress);
            return new SuccessResult(CorporateMailAddressMessages.Added);
        }

        [SecurityAspect("CorporateMailAddress.Delete", Priority = 2)]
        [CacheRemoveAspect("ICorporateMailAddressService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var corporateMailAddress = await _corporateMailAddressDal.Get(x => x.Id == id && x.IsActive == true);
            if (corporateMailAddress != null)
            {
                corporateMailAddress.IsActive = false;
                await _corporateMailAddressDal.Update(corporateMailAddress);
            }
            return new SuccessResult(CorporateMailAddressMessages.Deleted);
        }

        [SecurityAspect("CorporateMailAddress.GetById", Priority = 2)]
        public async Task<IDataResult<CorporateMailAddressGetByIdResponseDto>> GetById(int id)
        {
            var corporateMailAddress = await _corporateMailAddressDal.Get(x => x.Id == id && x.IsActive == true);
            var corporateMailAddressGetByIdResponseDto = _mapper.Map<CorporateMailAddressGetByIdResponseDto>(corporateMailAddress);
            return new SuccessDataResult<CorporateMailAddressGetByIdResponseDto>(corporateMailAddressGetByIdResponseDto);
        }

        [SecurityAspect("CorporateMailAddress.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<CorporateMailAddressGetListResponseDto>>> GetList()
        {
            var corporateMailAddresss = await _corporateMailAddressDal.GetList(x => x.IsActive == true);
            var corporateMailAddressGetListResponseDtos = _mapper.Map<List<CorporateMailAddressGetListResponseDto>>(corporateMailAddresss);
            corporateMailAddressGetListResponseDtos = corporateMailAddressGetListResponseDtos.OrderBy(x => x.Optime).OrderBy(x => x.CorporateId).ToList();
            return new SuccessDataResult<List<CorporateMailAddressGetListResponseDto>>(corporateMailAddressGetListResponseDtos);
        }

        [SecurityAspect("CorporateMailAddress.Save", Priority = 2)]
        [ValidationAspect(typeof(CorporateMailAddressSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ICorporateMailAddressService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(CorporateMailAddressSaveRequestDto corporateMailAddressSaveRequestDto)
        {
            var corporateMailAddress = corporateMailAddressSaveRequestDto.CorporateMailAddress;
            var saveType = corporateMailAddressSaveRequestDto.SaveType;
            if (saveType == SaveTypeEnum.Add)
            {
                corporateMailAddress.IsActive = true;
                corporateMailAddress.Optime = DateTime.Now;
                await _corporateMailAddressDal.Add(corporateMailAddress);

                return new SuccessResult(CorporateMailAddressMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                corporateMailAddress.IsActive = true;
                corporateMailAddress.Optime = DateTime.Now;
                await _corporateMailAddressDal.Update(corporateMailAddress);

                return new SuccessResult(CorporateMailAddressMessages.Updated);
            }

            return new ErrorResult(CorporateMailAddressMessages.OperationFailed);
        }

        [SecurityAspect("CorporateMailAddress.Search", Priority = 2)]
        public async Task<IDataResult<List<CorporateMailAddressSearchResponseDto>>> Search(CorporateMailAddressSearchRequestDto corporateMailAddressSearchRequestDto)
        {
            return new SuccessDataResult<List<CorporateMailAddressSearchResponseDto>>(await _corporateMailAddressDal.Search(corporateMailAddressSearchRequestDto));
        }

        [SecurityAspect("CorporateMailAddress.Update", Priority = 2)]
        [ValidationAspect(typeof(CorporateMailAddressUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("ICorporateMailAddressService.Get", Priority = 4)]
        public async Task<IResult> Update(CorporateMailAddressUpdateRequestDto corporateMailAddressUpdateRequestDto)
        {
            var corporateMailAddress = _mapper.Map<CorporateMailAddress>(corporateMailAddressUpdateRequestDto);
            if (corporateMailAddress != null)
            {
                await _corporateMailAddressDal.Update(corporateMailAddress);
                return new SuccessResult(CorporateMailAddressMessages.Updated);

            }
            return new ErrorResult(CorporateMailAddressMessages.OperationFailed);

        }

        [SecurityAspect("CorporateMailAddress.GetByCorporateId", Priority = 2)]
        public async Task<IDataResult<List<CorporateMailAddressGetListByCorporateIdResponseDto>>> GetByCorporateId(int CorporateId)
        {
            return new SuccessDataResult<List<CorporateMailAddressGetListByCorporateIdResponseDto>>(await _corporateMailAddressDal.GetByCorporateId(CorporateId));
        }















    }
}
