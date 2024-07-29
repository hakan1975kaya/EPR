using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleAddDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetByIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSaveDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleUpdateDtos;

namespace Business.Abstract
{
    public interface IUserRoleService
    {
        Task<IDataResult<UserRoleGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<UserRoleGetListResponseDto>>> GetList();
        Task<IResult> Add(UserRoleAddRequestDto userRoleAddRequestDto);
        Task<IResult> Update(UserRoleUpdateRequestDto userRoleUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<UserRoleSearchResponseDto>>> Search(UserRoleSearchRequestDto userRoleSearchRequestDto);
        Task<IResult> Save(UserRoleSaveRequestDto userRoleSaveRequestDto);
        Task<IDataResult<List<UserRoleGetListByRoleIdResponseDto>>> GetByRoleId(int roleId);
    }
}
