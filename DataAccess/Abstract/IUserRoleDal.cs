using Core.DataAccess.Abstract;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos;
using Entities.Concrete.Entities;

namespace DataAccess.Abstract
{
    public interface IUserRoleDal : IEntityRepository<UserRole>
    {
        Task<List<UserRoleSearchResponseDto>> Search(UserRoleSearchRequestDto userRoleSearchRequestDto);
        Task<List<UserRoleGetListByRoleIdResponseDto>> GetByRoleId(int roleId);
    }
}
