using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.MenuDtos.MenuChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuParentListGetByUserIdResponseDtos;
using Entities.Concrete.Entities;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFMenuDal : EFEntityRepositoryBase<Menu, EPRContext>, IMenuDal
    {
        public async Task<List<MenuChildListGetByUserIdResponseDto>> MenuChildListGetByUserId(int userId)
        {
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles on ur.RoleId equals r.Id
                             join roc in context.RoleOperationClaims on r.Id equals roc.RoleId
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             join moc in context.MenuOperationClaims on oc.Id equals moc.OperationClaimId
                             join m in context.Menus on moc.MenuId equals m.Id
                             where ur.IsActive == true && 
                             r.IsActive == true && 
                             roc.IsActive == true && 
                             oc.IsActive == true && 
                             moc.IsActive == true && 
                             m.IsActive == true && 
                             ur.UserId == userId &&
                             m.LinkedMenuId != 0
                             select new MenuChildListGetByUserIdResponseDto
                             {
                                 Description = m.Description,
                                 Id = m.Id,
                                 IsActive = m.IsActive,
                                 LinkedMenuId = m.LinkedMenuId,
                                 MenuOrder = m.MenuOrder,
                                 Name = m.Name,
                                 Path = m.Path,
                             };
                return result.OrderBy(x => x.MenuOrder).ToList();
            }
        }

        public async Task<List<MenuListGetByUserIdResponseDto>> MenuListGetByUserId(int userId)
        {
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles on ur.RoleId equals r.Id
                             join roc in context.RoleOperationClaims on r.Id equals roc.RoleId
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             join moc in context.MenuOperationClaims on oc.Id equals moc.OperationClaimId
                             join m in context.Menus on moc.MenuId equals m.Id
                             where ur.IsActive == true &&
                             r.IsActive == true &&
                             roc.IsActive == true &&
                             oc.IsActive == true &&
                             moc.IsActive == true &&
                             m.IsActive == true &&
                             ur.UserId == userId
                             select new MenuListGetByUserIdResponseDto
                             {
                                 Description = m.Description,
                                 Id = m.Id,
                                 IsActive = m.IsActive,
                                 LinkedMenuId = m.LinkedMenuId,
                                 MenuOrder = m.MenuOrder,
                                 Name = m.Name,
                                 Path = m.Path,
                             };
                return result.OrderBy(x => x.MenuOrder).ToList();
            }
        }

        public async Task<List<MenuParentListGetByUserIdResponseDto>> MenuParentListGetByUserId(int userId)
        {
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles on ur.RoleId equals r.Id
                             join roc in context.RoleOperationClaims on r.Id equals roc.RoleId
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             join moc in context.MenuOperationClaims on oc.Id equals moc.OperationClaimId
                             join m in context.Menus on moc.MenuId equals m.Id
                             where ur.IsActive == true &&
                             r.IsActive == true &&
                             roc.IsActive == true &&
                             oc.IsActive == true &&
                             moc.IsActive == true &&
                             m.IsActive == true &&
                             ur.UserId == userId &&
                             m.LinkedMenuId == 0
                             select new MenuParentListGetByUserIdResponseDto
                             {
                                 Description = m.Description,
                                 Id = m.Id,
                                 IsActive = m.IsActive,
                                 LinkedMenuId = m.LinkedMenuId,
                                 MenuOrder = m.MenuOrder,
                                 Name = m.Name,
                                 Path = m.Path,
                             };
                return result.OrderBy(x => x.MenuOrder).ToList();
            }
        }




    }
}
