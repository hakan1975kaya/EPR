using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos;
using Entities.Concrete.Entities;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFUserRoleDal : EFEntityRepositoryBase<UserRole, EPRContext>, IUserRoleDal
    {
        public async Task<List<UserRoleGetListByRoleIdResponseDto>> GetByRoleId(int roleId)
        {
            using (var context = new EPRContext())
            {

                var result = from ur in context.UserRoles
                             join u in context.Users on ur.UserId equals u.Id
                             join r in context.Roles on ur.RoleId equals r.Id
                             where
                             ur.IsActive == true &&
                             u.IsActive == true &&
                             r.IsActive == true &&
                             ur.RoleId == roleId
                             select new UserRoleGetListByRoleIdResponseDto
                             {
                                 Id = ur.Id,
                                 FirstName = u.FirstName,
                                 IsActive = ur.IsActive,
                                 LastName = u.LastName,
                                 RoleName = r.Name,
                                 Optime = ur.Optime,
                                 RegistrationNumber = u.RegistrationNumber,
                                 RoleId = ur.RoleId,
                                 UserId = ur.UserId
                             };


                return result.Distinct().OrderBy(x => x.RoleName).ToList();
            }
        }

        public async Task<List<UserRoleSearchResponseDto>> Search(UserRoleSearchRequestDto UserRoleSearchRequestDto)
        {
            var filter = UserRoleSearchRequestDto.Filter;
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join u in context.Users on ur.UserId equals u.Id
                             join r in context.Roles on ur.RoleId equals r.Id
                             where
                             ur.IsActive == true &&
                             u.IsActive == true &&
                             r.IsActive == true &&
                             (u.RegistrationNumber.ToString().ToLower().Contains(filter) ||
                             u.FirstName.ToLower().Contains(filter) ||
                             u.LastName.ToLower().Contains(filter) ||
                             r.Name.ToLower().Contains(filter))
                             select new UserRoleSearchResponseDto
                             {
                                 Id = ur.Id,
                                 FirstName = u.FirstName,
                                 IsActive = ur.IsActive,
                                 LastName = u.LastName,
                                 RoleName = r.Name,
                                 Optime = ur.Optime,
                                 RegistrationNumber = u.RegistrationNumber,
                                 RoleId = ur.RoleId,
                                 UserId = ur.UserId
                             };

                return result.Distinct().OrderBy(x => x.RoleName).ToList();
            }
        }
    }
}