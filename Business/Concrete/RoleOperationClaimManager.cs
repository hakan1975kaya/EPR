using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.RoleOperationClaimValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimAddDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetListDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimSaveDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimUpdateDtos;
using Entities.Concrete.Enums.GeneralEnums;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class RoleOperationClaimManager : IRoleOperationClaimService
    {
        private IRoleOperationClaimDal _roleOperationClaimDal;
        private IMapper _mapper;
        public RoleOperationClaimManager(IRoleOperationClaimDal roleOperationClaimDal, IMapper mapper)
        {
            _roleOperationClaimDal = roleOperationClaimDal;
            _mapper = mapper;
        }

        [SecurityAspect("RoleOperationClaim.Add", Priority = 2)]
        [ValidationAspect(typeof(RoleOperationClaimAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IRoleOperationClaimService.Get", Priority = 4)]
        public async Task<IResult> Add(RoleOperationClaimAddRequestDto roleOperationClaimAddRequestDto)
        {
            var roleOperationClaim = _mapper.Map<RoleOperationClaim>(roleOperationClaimAddRequestDto);
            await _roleOperationClaimDal.Add(roleOperationClaim);
            return new SuccessResult(RoleOperationClaimMessages.Added);
        }

        [SecurityAspect("RoleOperationClaim.Delete", Priority = 2)]
        [CacheRemoveAspect("IRoleOperationClaimService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var roleOperationClaim = await _roleOperationClaimDal.Get(x => x.Id == id && x.IsActive == true);
            if (roleOperationClaim != null)
            {
                roleOperationClaim.IsActive = false;
                roleOperationClaim.Optime = DateTime.Now;
                await _roleOperationClaimDal.Update(roleOperationClaim);
            }
            return new SuccessResult(RoleOperationClaimMessages.Deleted);
        }

        [SecurityAspect("RoleOperationClaim.GetById", Priority = 2)]
        public async Task<IDataResult<RoleOperationClaimGetByIdResponseDto>> GetById(int id)
        {
            var roleOperationClaim = await _roleOperationClaimDal.Get(x => x.Id == id && x.IsActive == true);
            var roleOperationClaimGetByIdResponseDto = _mapper.Map<RoleOperationClaimGetByIdResponseDto>(roleOperationClaim);
            return new SuccessDataResult<RoleOperationClaimGetByIdResponseDto>(roleOperationClaimGetByIdResponseDto);
        }

        [SecurityAspect("RoleOperationClaim.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<RoleOperationClaimGetListResponseDto>>> GetList()
        {
            var roleOperationClaims = await _roleOperationClaimDal.GetList(x => x.IsActive == true);
            var roleOperationClaimGetListResponseDtos = _mapper.Map<List<RoleOperationClaimGetListResponseDto>>(roleOperationClaims);
            roleOperationClaimGetListResponseDtos = roleOperationClaimGetListResponseDtos.OrderBy(x => x.RoleId).OrderBy(x => x.OperationClaimId).ToList();
            return new SuccessDataResult<List<RoleOperationClaimGetListResponseDto>>(roleOperationClaimGetListResponseDtos);
        }

        [SecurityAspect("RoleOperationClaim.Save", Priority = 2)]
        [ValidationAspect(typeof(RoleOperationClaimSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IRoleOperationClaimService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(RoleOperationClaimSaveRequestDto roleOperationClaimSaveRequestDto)
        {
            var role = roleOperationClaimSaveRequestDto.Role;
            var operationClaims = roleOperationClaimSaveRequestDto.OperationClaims;
            var saveType = roleOperationClaimSaveRequestDto.SaveType;
            if (saveType == SaveTypeEnum.Add)
            {
                foreach (var operationClaim in operationClaims)
                {
                    var roleOperationClaim = new RoleOperationClaim
                    {
                        RoleId = role.Id,
                        OperationClaimId = operationClaim.Id,
                        IsActive = true,
                        Optime = DateTime.Now
                    };

                    await _roleOperationClaimDal.Add(roleOperationClaim);
                }

                return new SuccessResult(RoleOperationClaimMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                var currentRoleOperationClaims = await _roleOperationClaimDal.GetList(x => x.RoleId == role.Id && x.IsActive == true);
                var currentOperationClaimIds = currentRoleOperationClaims.Select(x => x.OperationClaimId).ToList();
                var selectedOperationClaimIds = operationClaims.Select(x => x.Id).ToList();
                var addedOperationClaims = operationClaims.Where(x => !currentOperationClaimIds.Contains(x.Id)).ToList();
                var deletedOperationClaims = currentRoleOperationClaims.Where(x => !selectedOperationClaimIds.Contains(x.OperationClaimId)).ToList();

                foreach (var addedOperationClaim in addedOperationClaims)
                {
                    var roleOperationClaim = new RoleOperationClaim
                    {
                        RoleId = role.Id,
                        OperationClaimId = addedOperationClaim.Id,
                        IsActive = true,
                        Optime = DateTime.Now
                    };

                    await _roleOperationClaimDal.Add(roleOperationClaim);
                }

                foreach (var deletedOperationClaim in deletedOperationClaims)
                {
                    var roleOperationClaims = await _roleOperationClaimDal.GetList(x => x.RoleId == role.Id && x.OperationClaimId == deletedOperationClaim.OperationClaimId && x.IsActive == true);

                    if (roleOperationClaims != null)
                    {
                        if (roleOperationClaims.Count() > 0)
                        {
                            foreach (var roleOperationClaim in roleOperationClaims)
                            {
                                roleOperationClaim.IsActive = false;
                                roleOperationClaim.Optime = DateTime.Now;
                                await _roleOperationClaimDal.Update(roleOperationClaim);
                            }
                        }
                    }
                }

                return new SuccessResult(RoleOperationClaimMessages.Updated);
            }

            return new ErrorResult(RoleOperationClaimMessages.OperationFailed);
        }

        [SecurityAspect("RoleOperationClaim.Update", Priority = 2)]
        [ValidationAspect(typeof(RoleOperationClaimUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IRoleOperationClaimService.Get", Priority = 4)]
        public async Task<IResult> Update(RoleOperationClaimUpdateRequestDto roleOperationClaimUpdateRequestDto)
        {
            var roleOperationClaim = _mapper.Map<RoleOperationClaim>(roleOperationClaimUpdateRequestDto);
            if (roleOperationClaim != null)
            {
                await _roleOperationClaimDal.Update(roleOperationClaim);
                return new SuccessResult(RoleOperationClaimMessages.Updated);

            }
            return new ErrorResult(RoleOperationClaimMessages.OperationFailed);

        }

















    }
}
