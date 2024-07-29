using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.RoleValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Core.Entities.Concrete.Entities;
using Business.ValidationRules.FluentValidation.RoleDetailValidators;
using Entities.Concrete.Enums.GeneralEnums;
using Business.ValidationRules.FluentValidation.MenuValidators;
using Entities.Concrete.Dtos.RoleDtos.RoleAddDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleGetByIdDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleGetListDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleUpdateDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos;
using Core.Aspects.Autofac.Transaction;
using Entities.Concrete.Dtos.RoleDtos.RoleSaveDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class RoleManager:IRoleService
    {
        private IRoleDal _roleDal;
        private IMapper _mapper;
        public RoleManager(IRoleDal roleDal,IMapper mapper)
        {
            _roleDal = roleDal;
            _mapper = mapper;
        }

        [SecurityAspect("Role.Add",Priority=2)]
        [ValidationAspect(typeof(RoleAddRequestDtoValidator),Priority =3)]
        [CacheRemoveAspect("IRoleService.Get",Priority =4)]
        public async Task<IResult> Add(RoleAddRequestDto roleAddRequestDto)
        {
            var roleAdd = _mapper.Map<Role>(roleAddRequestDto);
            await _roleDal.Add(roleAdd);
            return new SuccessResult(RoleMessages.Added);
;        }

        [SecurityAspect("Role.Delete", Priority = 2)]
        [CacheRemoveAspect("IRoleService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var role = await _roleDal.Get(x => x.Id == id && x.IsActive == true);
            if (role != null)
            {
                role.IsActive = false;
                await _roleDal.Update(role);
                return new SuccessResult(RoleMessages.Deleted);

            }
            return new ErrorResult(RoleMessages.OperationFailed);
        }

        [SecurityAspect("Role.GetById", Priority = 2)]
        public async Task<IDataResult<RoleGetByIdResponseDto>> GetById(int id)
        {

            var role = await _roleDal.Get(x => x.Id == id && x.IsActive == true);
            var roleGetByIdResponseDto = _mapper.Map<RoleGetByIdResponseDto>(role);
            return new SuccessDataResult<RoleGetByIdResponseDto>(roleGetByIdResponseDto);
        }

        [SecurityAspect("Role.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public  async Task<IDataResult<List<RoleGetListResponseDto>>> GetList()
        {
            var roles = await _roleDal.GetList(x => x.IsActive == true);
            var roleGetListResponseDtos = _mapper.Map<List<RoleGetListResponseDto>>(roles);
            roleGetListResponseDtos= roleGetListResponseDtos.OrderBy(x => x.Name).ToList();
            return new SuccessDataResult<List<RoleGetListResponseDto>>(roleGetListResponseDtos);
        }

        [SecurityAspect("Role.Update", Priority = 2)]
        [ValidationAspect(typeof(RoleUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IRoleService.Get", Priority = 4)]
        public async Task<IResult> Update(RoleUpdateRequestDto roleUpdateRequestDto)
        {
            var role = _mapper.Map<Role>(roleUpdateRequestDto);
            if (role != null)
            {
                await _roleDal.Update(role);
                return new SuccessResult(RoleMessages.Updated);
            }

            return new ErrorResult(RoleMessages.OperationFailed);
        }

        [SecurityAspect("Role.Search", Priority = 2)]
        [ValidationAspect(typeof(RoleSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<RoleSearchResponseDto>>> Search(RoleSearchRequestDto roleSearchRequestDto)
        {
            var filter = roleSearchRequestDto.Filter;
            var roles = await _roleDal.GetList(x => ( x.Name.ToLower().Contains(filter.ToLower())) && x.IsActive == true);
            var roleSearchResponseDtos = _mapper.Map<List<RoleSearchResponseDto>>(roles.OrderBy(x => x.Name));
            return new SuccessDataResult<List<RoleSearchResponseDto>>(roleSearchResponseDtos);
        }


        [SecurityAspect("Role.Save", Priority = 2)]
        [ValidationAspect(typeof(RoleSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IRoleService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(RoleSaveRequestDto roleSaveRequestDto)
        {
            var saveType = roleSaveRequestDto.SaveType;
            var role = roleSaveRequestDto.Role;
            if (saveType == SaveTypeEnum.Add)
            {
                role.IsActive = true;
                role.Optime = DateTime.Now;
                await _roleDal.Add(role);

                return new SuccessResult(RoleMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                role.IsActive = true;
                role.Optime = DateTime.Now;
                await _roleDal.Update(role);

                return new SuccessResult(RoleMessages.Updated);
            }

            return new ErrorResult(RoleMessages.OperationFailed);
        }

    }
}
