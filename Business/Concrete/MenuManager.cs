using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.MenuValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.MenuDtos.MenuAddDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuGetByIdDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuGetListDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSaveDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuUpdateDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class MenuManager : IMenuService
    {
        private IMenuDal _menuDal;
        private IMapper _mapper;
        public MenuManager(IMenuDal menuDal, IMapper mapper)
        {
            _menuDal = menuDal;
            _mapper = mapper;

        }

        [SecurityAspect("Menu.Add", Priority = 2)]
        [ValidationAspect(typeof(MenuAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 4)]
        public async Task<IResult> Add(MenuAddRequestDto menuAddRequestDto)
        {
            var menuAdd = _mapper.Map<Menu>(menuAddRequestDto);
            await _menuDal.Add(menuAdd);
            return new SuccessResult(MenuMessages.Added);
            ;
        }

        [SecurityAspect("Menu.Delete", Priority = 2)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var menu = await _menuDal.Get(x => x.Id == id && x.IsActive == true);
            if (menu != null)
            {
                menu.IsActive = false;
                await _menuDal.Update(menu);
                return new SuccessResult(MenuMessages.Deleted);

            }
            return new ErrorResult(MenuMessages.OperationFailed);

        }
        public async Task<IDataResult<MenuGetByIdResponseDto>> GetById(int id)
        {

            var menu = await _menuDal.Get(x => x.Id == id && x.IsActive == true );
            var menuGetByIdResponseDto = _mapper.Map<MenuGetByIdResponseDto>(menu);
            return new SuccessDataResult<MenuGetByIdResponseDto>(menuGetByIdResponseDto);
        }

        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<MenuGetListResponseDto>>> GetList()
        {
            var menus = await _menuDal.GetList(x=>x.IsActive == true);
            var menuGetListResponseDtos = _mapper.Map<List<MenuGetListResponseDto>>(menus);
            return new SuccessDataResult<List<MenuGetListResponseDto>>(menuGetListResponseDtos);
        }

        [SecurityAspect("Menu.Update", Priority = 2)]
        [ValidationAspect(typeof(MenuUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 4)]
        public async Task<IResult> Update(MenuUpdateRequestDto menuUpdateRequestDto)
        {
            var menu = _mapper.Map<Menu>(menuUpdateRequestDto);
            if (menu != null)
            {
                await _menuDal.Update(menu);
                return new SuccessResult(MenuMessages.Updated);
            }

            return new ErrorResult(MenuMessages.OperationFailed);
        }
        public async Task<IDataResult<List<MenuParentListGetByUserIdResponseDto>>> MenuParentListGetByUserId(int userId)
        {
            return new SuccessDataResult<List<MenuParentListGetByUserIdResponseDto>>(await _menuDal.MenuParentListGetByUserId(userId));
        }

        public async Task<IDataResult<List<MenuChildListGetByUserIdResponseDto>>> MenuChildListGetByUserId(int userId)
        {
            return new SuccessDataResult<List<MenuChildListGetByUserIdResponseDto>>(await _menuDal.MenuChildListGetByUserId(userId));
        }

        [SecurityAspect("Menu.Save", Priority = 2)]
        [ValidationAspect(typeof(MenuSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMenuService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(MenuSaveRequestDto menuSaveRequestDto)
        {
            var saveType = menuSaveRequestDto.SaveType;
            var menu = menuSaveRequestDto.Menu;
            if (saveType == SaveTypeEnum.Add)
            {
                menu.IsActive = true;
                await _menuDal.Add(menu);

                return new SuccessResult(MenuMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                menu.IsActive = true;

                await _menuDal.Update(menu);

                return new SuccessResult(MenuMessages.Updated);
            }

            return new ErrorResult(MenuMessages.OperationFailed);
        }

        [SecurityAspect("Menu.Search", Priority = 2)]
        [ValidationAspect(typeof(MenuSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<MenuSearchResponseDto>>> Search(MenuSearchRequestDto menuSearchRequestDto)
        {
            var filter = menuSearchRequestDto.Filter;
            var menus = await _menuDal.GetList(x => (x.Name.ToLower().Contains(filter.ToLower()) || x.Description.ToLower().Contains(filter.ToLower()) || x.Path.ToLower().Contains(filter.ToLower())) && x.IsActive == true);
            var menuSearchResponseDtos = _mapper.Map<List<MenuSearchResponseDto>>(menus.OrderBy(x => x.Name));
            return new SuccessDataResult<List<MenuSearchResponseDto>>(menuSearchResponseDtos);
        }
    }
}
