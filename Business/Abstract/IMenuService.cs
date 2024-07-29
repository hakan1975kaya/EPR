using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.MenuDtos.MenuAddDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuGetByIdDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuGetListDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuUpdateDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSaveDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;

namespace Business.Abstract
{
    public interface IMenuService
    {
        Task<IDataResult<MenuGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<MenuGetListResponseDto>>> GetList();
        Task<IResult> Add(MenuAddRequestDto menuAddRequestDto);
        Task<IResult> Update(MenuUpdateRequestDto menuUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<MenuParentListGetByUserIdResponseDto>>> MenuParentListGetByUserId(int userId);
        Task<IDataResult<List<MenuChildListGetByUserIdResponseDto>>> MenuChildListGetByUserId(int userId);
        Task<IDataResult<List<MenuSearchResponseDto>>> Search(MenuSearchRequestDto menuSearchRequestDto);
        Task<IResult> Save(MenuSaveRequestDto menuSaveRequestDto);
    }
}

