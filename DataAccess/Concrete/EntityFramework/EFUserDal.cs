using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFUserDal : EFEntityRepositoryBase<User, EPRContext>, IUserDal
    {
        public async Task<List<OperationClaim>> GetClaims(User user)
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
                             ur.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = oc.Id,
                                 Name = oc.Name
                             };

                return result.ToList();
            }
        }



    }
}

