using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.UserRoleValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleAddDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetByIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSaveDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleUpdateDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class UserRoleManager : IUserRoleService
    {
        private IUserRoleDal _userRoleDal;
        private IMapper _mapper;
        public UserRoleManager(IUserRoleDal userRoleDal, IMapper mapper)
        {
            _userRoleDal = userRoleDal;
            _mapper = mapper;
        }

        [SecurityAspect("UserRole.Add", Priority = 2)]
        [ValidationAspect(typeof(UserRoleAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserRoleService.Get", Priority = 4)]
        public async Task<IResult> Add(UserRoleAddRequestDto userRoleAddRequestDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleAddRequestDto);
            await _userRoleDal.Add(userRole);
            return new SuccessResult(UserRoleMessages.Added);
        }

        [SecurityAspect("UserRole.Delete", Priority = 2)]
        [CacheRemoveAspect("IUserRoleService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var userRole = await _userRoleDal.Get(x => x.Id == id && x.IsActive == true);
            if (userRole != null)
            {
                userRole.IsActive = false;
                userRole.Optime = DateTime.Now;
                await _userRoleDal.Update(userRole);
            }
            return new SuccessResult(UserRoleMessages.Deleted);
        }

        [SecurityAspect("UserRole.GetById", Priority = 2)]
        public async Task<IDataResult<UserRoleGetByIdResponseDto>> GetById(int id)
        {
            var userRole = await _userRoleDal.Get(x => x.Id == id && x.IsActive == true);
            var userRoleGetByIdResponseDto = _mapper.Map<UserRoleGetByIdResponseDto>(userRole);
            return new SuccessDataResult<UserRoleGetByIdResponseDto>(userRoleGetByIdResponseDto);
        }

        [SecurityAspect("UserRole.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<UserRoleGetListResponseDto>>> GetList()
        {
            var userRoles = await _userRoleDal.GetList(x => x.IsActive == true);
            var userRoleGetListResponseDtos = _mapper.Map<List<UserRoleGetListResponseDto>>(userRoles);
            userRoleGetListResponseDtos = userRoleGetListResponseDtos.OrderBy(x => x.Optime).OrderBy(x => x.RoleId).ToList();
            return new SuccessDataResult<List<UserRoleGetListResponseDto>>(userRoleGetListResponseDtos);
        }

        [SecurityAspect("UserRole.Save", Priority = 2)]
        [ValidationAspect(typeof(UserRoleSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserRoleService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(UserRoleSaveRequestDto userRoleSaveRequestDto)
        {
            var userRole = userRoleSaveRequestDto.UserRole;
            var saveType = userRoleSaveRequestDto.SaveType;
            if (saveType == SaveTypeEnum.Add)
            {
                userRole.IsActive = true;
                userRole.Optime = DateTime.Now;
                await _userRoleDal.Add(userRole);

                return new SuccessResult(UserRoleMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                userRole.IsActive = true;
                userRole.Optime = DateTime.Now;
                await _userRoleDal.Update(userRole);

                return new SuccessResult(UserRoleMessages.Updated);
            }

            return new ErrorResult(UserRoleMessages.OperationFailed);
        }

        [SecurityAspect("UserRole.Search", Priority = 2)]
        public async Task<IDataResult<List<UserRoleSearchResponseDto>>> Search(UserRoleSearchRequestDto userRoleSearchRequestDto)
        {
            return new SuccessDataResult<List<UserRoleSearchResponseDto>>(await _userRoleDal.Search(userRoleSearchRequestDto));
        }

        [SecurityAspect("UserRole.Update", Priority = 2)]
        [ValidationAspect(typeof(UserRoleUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserRoleService.Get", Priority = 4)]
        public async Task<IResult> Update(UserRoleUpdateRequestDto userRoleUpdateRequestDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleUpdateRequestDto);
            if (userRole != null)
            {
                await _userRoleDal.Update(userRole);
                return new SuccessResult(UserRoleMessages.Updated);

            }
            return new ErrorResult(UserRoleMessages.OperationFailed);

        }

        [SecurityAspect("UserRole.GetByRoleId", Priority = 2)]
        public async Task<IDataResult<List<UserRoleGetListByRoleIdResponseDto>>> GetByRoleId(int roleId)
        {
            return new SuccessDataResult<List<UserRoleGetListByRoleIdResponseDto>>(await _userRoleDal.GetByRoleId(roleId));
        }















    }
}
