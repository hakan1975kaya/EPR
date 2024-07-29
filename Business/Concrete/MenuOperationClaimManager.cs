using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.MenuOperationClaimValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetListDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimSaveDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimUpdateDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class MenuOperationClaimManager : IMenuOperationClaimService
    {
        private IMenuOperationClaimDal _menuOperationClaimDal;
        private IMapper _mapper;
        public MenuOperationClaimManager(IMenuOperationClaimDal menuOperationClaimDal, IMapper mapper)
        {
            _menuOperationClaimDal = menuOperationClaimDal;
            _mapper = mapper;
        }

        [SecurityAspect("MenuOperationClaim.Add", Priority = 2)]
        [ValidationAspect(typeof(MenuOperationClaimAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuOperationClaimService.Get", Priority = 4)]
        public async Task<IResult> Add(MenuOperationClaimAddRequestDto menuOperationClaimAddRequestDto)
        {
            var menuOperationClaimAdd = _mapper.Map<MenuOperationClaim>(menuOperationClaimAddRequestDto);
            await _menuOperationClaimDal.Add(menuOperationClaimAdd);
            return new SuccessResult(MenuOperationClaimMessages.Added);
            ;
        }

        [SecurityAspect("MenuOperationClaim.Delete", Priority = 2)]
        [CacheRemoveAspect("IMenuOperationClaimService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var menuOperationClaim = await _menuOperationClaimDal.Get(x => x.Id == id && x.IsActive == true);
            if (menuOperationClaim != null)
            {
                menuOperationClaim.IsActive = false;
                await _menuOperationClaimDal.Update(menuOperationClaim);
                return new SuccessResult(MenuOperationClaimMessages.Deleted);
            }
            return new ErrorResult(MenuOperationClaimMessages.OperationFailed);
        }

        [SecurityAspect("MenuOperationClaim.GetById", Priority = 2)]
        public async Task<IDataResult<MenuOperationClaimGetByIdResponseDto>> GetById(int id)
        {
            var menuOperationClaim = await _menuOperationClaimDal.Get(x => x.Id == id && x.IsActive == true);
            var menuOperationClaimGetByIdResponseDto = _mapper.Map<MenuOperationClaimGetByIdResponseDto>(menuOperationClaim);
            return new SuccessDataResult<MenuOperationClaimGetByIdResponseDto>(menuOperationClaimGetByIdResponseDto);
        }

        [SecurityAspect("MenuOperationClaim.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<MenuOperationClaimGetListResponseDto>>> GetList()
        {
            var menuOperationClaims = await _menuOperationClaimDal.GetList(x => x.IsActive == true);
            var menuOperationClaimGetListResponseDtos = _mapper.Map<List<MenuOperationClaimGetListResponseDto>>(menuOperationClaims);
            menuOperationClaimGetListResponseDtos = menuOperationClaimGetListResponseDtos.OrderBy(x => x.MenuId).ToList();
            return new SuccessDataResult<List<MenuOperationClaimGetListResponseDto>>(menuOperationClaimGetListResponseDtos);
        }

        [SecurityAspect("MenuOperationClaim.Save", Priority = 2)]
        [ValidationAspect(typeof(MenuOperationClaimSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuOperationClaimService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(MenuOperationClaimSaveRequestDto menuOperationClaimSaveRequestDto)
        {
            var menu = menuOperationClaimSaveRequestDto.Menu;
            var operationClaims = menuOperationClaimSaveRequestDto.OperationClaims;
            var saveType = menuOperationClaimSaveRequestDto.SaveType;
            if (saveType == SaveTypeEnum.Add)
            {
                foreach (var operationClaim in operationClaims)
                {
                    var menuOperationClaim = new MenuOperationClaim
                    {
                        MenuId = menu.Id,
                        OperationClaimId = operationClaim.Id,
                        IsActive = true,
                    };

                    await _menuOperationClaimDal.Add(menuOperationClaim);
                }

                return new SuccessResult(MenuOperationClaimMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                var currentMenuOperationClaims = await _menuOperationClaimDal.GetList(x => x.MenuId == menu.Id && x.IsActive == true);
                var currentOperationClaimIds = currentMenuOperationClaims.Select(x => x.OperationClaimId).ToList();
                var selectedOperationClaimIds = operationClaims.Select(x => x.Id).ToList();
                var addedOperationClaims = operationClaims.Where(x => !currentOperationClaimIds.Contains(x.Id)).ToList();
                var deletedOperationClaims = currentMenuOperationClaims.Where(x => !selectedOperationClaimIds.Contains(x.OperationClaimId)).ToList();

                foreach (var addedOperationClaim in addedOperationClaims)
                {
                    var menuOperationClaim = new MenuOperationClaim
                    {
                        MenuId = menu.Id,
                        OperationClaimId = addedOperationClaim.Id,
                        IsActive = true,
                    };
                    await _menuOperationClaimDal.Add(menuOperationClaim);
                }

                foreach (var deletedOperationClaim in deletedOperationClaims)
                {
                    var menuOperationClaims = await _menuOperationClaimDal.GetList(x => x.MenuId == menu.Id && x.OperationClaimId == deletedOperationClaim.OperationClaimId && x.IsActive == true);

                    if (menuOperationClaims != null)
                    {
                        if (menuOperationClaims.Count() > 0)
                        {
                            foreach (var menuOperationClaim in menuOperationClaims)
                            {
                                menuOperationClaim.IsActive = false;

                                await _menuOperationClaimDal.Update(menuOperationClaim);
                            }
                        }

                    }
                }

                return new SuccessResult(MenuOperationClaimMessages.Updated);
            }

            return new ErrorResult(MenuOperationClaimMessages.OperationFailed);
        }


        [SecurityAspect("MenuOperationClaim.Update", Priority = 2)]
        [ValidationAspect(typeof(MenuOperationClaimUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuOperationClaimService.Get", Priority = 4)]
        public async Task<IResult> Update(MenuOperationClaimUpdateRequestDto menuOperationClaimUpdateRequestDto)
        {
            var menuOperationClaim = _mapper.Map<MenuOperationClaim>(menuOperationClaimUpdateRequestDto);
            if (menuOperationClaim != null)
            {
                await _menuOperationClaimDal.Update(menuOperationClaim);
                return new SuccessResult(MenuOperationClaimMessages.Updated);
            }

            return new ErrorResult(MenuOperationClaimMessages.OperationFailed);

        }
    }
}
