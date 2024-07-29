using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByMenuIdResponseDtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFOperationClaimDal : EFEntityRepositoryBase<OperationClaim, EPRContext>, IOperationClaimDal
    {
        public async Task<List<OperationClaimChildListGetByMenuIdResponseDto>> OperationClaimChildListGetByMenuId(int menuId)
        {
            using (var context = new EPRContext())
            {
                var result = from moc in context.MenuOperationClaims
                             join oc in context.OperationClaims on moc.OperationClaimId equals oc.Id
                             where moc.IsActive == true &&
                             oc.IsActive == true &&
                             moc.MenuId == menuId &&
                             oc.LinkedOperationClaimId != 0
                             select new OperationClaimChildListGetByMenuIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<List<OperationClaimChildListGetByUserIdResponseDto>> OperationClaimChildListGetByRoleId(int roleId)
        {
            using (var context = new EPRContext())
            {
                var result = from roc in context.RoleOperationClaims
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             where roc.IsActive == true &&
                             oc.IsActive == true &&
                             roc.RoleId == roleId &&
                             oc.LinkedOperationClaimId != 0
                             select new OperationClaimChildListGetByUserIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<List<OperationClaimChildListGetByUserIdResponseDto>> OperationClaimChildListGetByUserId(int userId)
        {
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles on ur.RoleId equals r.Id
                             join roc in context.RoleOperationClaims on r.Id equals roc.RoleId
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             where ur.IsActive == true &&
                             r.IsActive == true &&
                             roc.IsActive == true &&
                             ur.UserId == userId &&
                             oc.LinkedOperationClaimId != 0
                             select new OperationClaimChildListGetByUserIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<List<OperationClaimGetListByUserIdResponseDto>> OperationClaimGetListByUserId(int userId)
        {
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles on ur.RoleId equals r.Id
                             join roc in context.RoleOperationClaims on r.Id equals roc.RoleId
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             where ur.IsActive == true &&
                             r.IsActive == true &&
                             roc.IsActive == true &&
                             oc.IsActive == true &&
                             ur.UserId == userId
                             select new OperationClaimGetListByUserIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<List<OperationClaimParentListGetByMenuIdResponseDto>> OperationClaimParentListGetByMenuId(int menuId)
        {
            using (var context = new EPRContext())
            {
                var result = from moc in context.MenuOperationClaims
                             join oc in context.OperationClaims on moc.OperationClaimId equals oc.Id
                             where moc.IsActive == true &&
                             oc.IsActive == true &&
                             moc.MenuId == menuId &&
                             oc.LinkedOperationClaimId == 0
                             select new OperationClaimParentListGetByMenuIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<List<OperationClaimParentListGetByUserIdResponseDto>> OperationClaimParentListGetByRoleId(int roleId)
        {
            using (var context = new EPRContext())
            {
                var result = from roc in context.RoleOperationClaims
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             where roc.IsActive == true &&
                             oc.IsActive == true &&
                             roc.RoleId == roleId &&
                             oc.LinkedOperationClaimId == 0
                             select new OperationClaimParentListGetByUserIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().Distinct().OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<List<OperationClaimParentListGetByUserIdResponseDto>> OperationClaimParentListGetByUserId(int userId)
        {
            using (var context = new EPRContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles on ur.RoleId equals r.Id
                             join roc in context.RoleOperationClaims on r.Id equals roc.RoleId
                             join oc in context.OperationClaims on roc.OperationClaimId equals oc.Id
                             where ur.IsActive == true &&
                             r.IsActive == true &&
                             roc.IsActive == true &&
                             oc.IsActive == true &&
                             ur.UserId == userId &&
                             oc.LinkedOperationClaimId == 0
                             select new OperationClaimParentListGetByUserIdResponseDto
                             {
                                 Id = oc.Id,
                                 IsActive = oc.IsActive,
                                 LinkedOperationClaimId = oc.LinkedOperationClaimId,
                                 Name = oc.Name
                             };

                return result.Distinct().Distinct().OrderBy(x => x.Name).ToList();
            }
        }
    }
}
