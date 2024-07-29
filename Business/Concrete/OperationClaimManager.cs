using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.OperationClaimValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimAddDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSaveDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSearchDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimUpdateDtos;
using Entities.Concrete.Enums.GeneralEnums;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByMenuIdResponseDtos;
using Business.ValidationRules.FluentValidation.CorporateValidators;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;
        private IMapper _mapper;
        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [SecurityAspect("OperationClaim.Add", Priority = 2)]
        [ValidationAspect(typeof(OperationClaimAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IOperationClaimService.Get", Priority = 4)]
        public async Task<IResult> Add(OperationClaimAddRequestDto operationClaimAddRequestDto)
        {
            var operationClaim = _mapper.Map<OperationClaim>(operationClaimAddRequestDto);
            await _operationClaimDal.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);
        }

        [SecurityAspect("OperationClaim.Delete", Priority = 2)]
        [CacheRemoveAspect("IOperationClaimService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var operationClaim = await _operationClaimDal.Get(x => x.Id == id && x.IsActive == true);
            if (operationClaim != null)
            {
                operationClaim.IsActive = false;
                await _operationClaimDal.Update(operationClaim);
                return new SuccessResult(OperationClaimMessages.Deleted);

            }

            return new ErrorResult(OperationClaimMessages.OperationFailed);
        }

        [SecurityAspect("OperationClaim.GetById", Priority = 2)]
        public async Task<IDataResult<OperationClaimGetByIdResponseDto>> GetById(int id)
        {
            var operationClaim = await _operationClaimDal.Get(x => x.Id == id && x.IsActive == true);
            var operationClaimGetByIdResponseDto = _mapper.Map<OperationClaimGetByIdResponseDto>(operationClaim);
            return new SuccessDataResult<OperationClaimGetByIdResponseDto>(operationClaimGetByIdResponseDto);
        }

        [SecurityAspect("OperationClaim.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<OperationClaimGetListResponseDto>>> GetList()
        {
            var operationClaims = await _operationClaimDal.GetList(x => x.IsActive == true);
            var OperationClaimGetListResponseDtos = _mapper.Map<List<OperationClaimGetListResponseDto>>(operationClaims);
            return new SuccessDataResult<List<OperationClaimGetListResponseDto>>(OperationClaimGetListResponseDtos.OrderBy(x => x.Name).ToList());
        }

        [SecurityAspect("OperationClaim.OperationClaimParentList", Priority = 2)]
        public async Task<IDataResult<List<OperationClaimParentListResponseDto>>> OperationClaimParentList()
        {
            var operationClaims = await _operationClaimDal.GetList(x => x.LinkedOperationClaimId == 0 && x.IsActive == true);
            var operationClaimParentListResponseDto = _mapper.Map<List<OperationClaimParentListResponseDto>>(operationClaims);

            return new SuccessDataResult<List<OperationClaimParentListResponseDto>>(operationClaimParentListResponseDto.OrderBy(x => x.Name).ToList());
        }

        [SecurityAspect("OperationClaim.OperationClaimChildList", Priority = 2)]
        public async Task<IDataResult<List<OperationClaimChildListResponseDto>>> OperationClaimChildList()
        {
            var operationClaims = await _operationClaimDal.GetList(x => x.LinkedOperationClaimId != 0 && x.IsActive == true);
            var operationClaimChildListResponseDto = _mapper.Map<List<OperationClaimChildListResponseDto>>(operationClaims);

            return new SuccessDataResult<List<OperationClaimChildListResponseDto>>(operationClaimChildListResponseDto.OrderBy(x => x.Name).ToList());
        }

        [SecurityAspect("OperationClaim.OperationClaimParentListGetByUserId", Priority = 2)]
        public async Task<IDataResult<List<OperationClaimParentListGetByUserIdResponseDto>>>OperationClaimParentListGetByUserId(int userId)
        {
            return new SuccessDataResult<List<OperationClaimParentListGetByUserIdResponseDto>>(await _operationClaimDal.OperationClaimParentListGetByUserId(userId));
        }

        [SecurityAspect("OperationClaim.OperationClaimChildListGetByUserId", Priority = 2)]
        public async Task<IDataResult<List<OperationClaimChildListGetByUserIdResponseDto>>>OperationClaimChildListGetByUserId(int userId)
        {
            return new SuccessDataResult<List<OperationClaimChildListGetByUserIdResponseDto>>(await _operationClaimDal.OperationClaimChildListGetByUserId(userId));
        }

        [SecurityAspect("OperationClaim.OperationClaimParentListGetByMenuId", Priority = 2)]
        public async Task<IDataResult<List<OperationClaimParentListGetByMenuIdResponseDto>>> OperationClaimParentListGetByMenuId(int MenuId)
        {
            return new SuccessDataResult<List<OperationClaimParentListGetByMenuIdResponseDto>>(await _operationClaimDal.OperationClaimParentListGetByMenuId(MenuId));
        }

        [SecurityAspect("OperationClaim.OperationClaimChildListGetByMenuId", Priority = 2)]
        public async Task<IDataResult<List<OperationClaimChildListGetByMenuIdResponseDto>>> OperationClaimChildListGetByMenuId(int MenuId)
        {
            return new SuccessDataResult<List<OperationClaimChildListGetByMenuIdResponseDto>>(await _operationClaimDal.OperationClaimChildListGetByMenuId(MenuId));
        }

        [SecurityAspect("OperationClaim.Save", Priority = 2)]
        [ValidationAspect(typeof(OperationClaimSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IOperationClaimService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(OperationClaimSaveRequestDto operationClaimSaveRequestDto)
        {
            var saveType = operationClaimSaveRequestDto.SaveType;
            var operationClaim = operationClaimSaveRequestDto.OperationClaim;
            if (saveType == SaveTypeEnum.Add)
            {
                operationClaim.IsActive = true;
                await _operationClaimDal.Add(operationClaim);

                return new SuccessResult(OperationClaimMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                operationClaim.IsActive = true;

                await _operationClaimDal.Update(operationClaim);

                return new SuccessResult(OperationClaimMessages.Updated);
            }

            return new ErrorResult(OperationClaimMessages.OperationFailed);

        }

        [SecurityAspect("OperationClaim.Search", Priority = 2)]
        [ValidationAspect(typeof(OperationClaimSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<OperationClaimSearchResponseDto>>> Search(OperationClaimSearchRequestDto operationClaimSearchRequestDto)
        {
            var filter = operationClaimSearchRequestDto.Filter;
            var operationClaims = await _operationClaimDal.GetList(x => x.Name.ToLower().Contains(filter.ToLower()) && x.IsActive == true);
            var operationClaimSearchResponseDtos = _mapper.Map<List<OperationClaimSearchResponseDto>>(operationClaims.OrderBy(x=>x.Name));
            return new SuccessDataResult<List<OperationClaimSearchResponseDto>>(operationClaimSearchResponseDtos);
        }

        [SecurityAspect("OperationClaim.Update", Priority = 2)]
        [ValidationAspect(typeof(OperationClaimUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IOperationClaimService.Get", Priority = 4)]
        public async Task<IResult> Update(OperationClaimUpdateRequestDto operationClaimUpdateRequestDto)
        {
            var operationClaim = _mapper.Map<OperationClaim>(operationClaimUpdateRequestDto);
            if (operationClaim != null)
            {
                await _operationClaimDal.Update(operationClaim);
                return new SuccessResult(OperationClaimMessages.Updated);
            }

            return new ErrorResult(OperationClaimMessages.OperationFailed);
        }

        public async Task<IDataResult<List<OperationClaimParentListGetByUserIdResponseDto>>> OperationClaimParentListGetByRoleId(int roleId)
        {
            return new SuccessDataResult<List<OperationClaimParentListGetByUserIdResponseDto>>(await _operationClaimDal.OperationClaimParentListGetByRoleId(roleId));
        }

        public async Task<IDataResult<List<OperationClaimChildListGetByUserIdResponseDto>>> OperationClaimChildListGetByRoleId(int roleId)
        {
            return new SuccessDataResult<List<OperationClaimChildListGetByUserIdResponseDto>>(await _operationClaimDal.OperationClaimChildListGetByRoleId(roleId));
        }
    }
}
