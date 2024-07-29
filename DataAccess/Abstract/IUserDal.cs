using Core.DataAccess.Abstract;
using Core.Entities.Concrete.Entities;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
    }
}
