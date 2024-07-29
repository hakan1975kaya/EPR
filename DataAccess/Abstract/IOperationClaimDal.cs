using Core.DataAccess.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByMenuIdResponseDtos;

namespace DataAccess.Abstract
{
    public interface IOperationClaimDal : IEntityRepository<OperationClaim>
    {
        Task<List<OperationClaimGetListByUserIdResponseDto>> OperationClaimGetListByUserId(int userId);
        Task<List<OperationClaimParentListGetByUserIdResponseDto>> OperationClaimParentListGetByUserId(int userId);
        Task<List<OperationClaimChildListGetByUserIdResponseDto>> OperationClaimChildListGetByUserId(int userId);
        Task<List<OperationClaimParentListGetByUserIdResponseDto>> OperationClaimParentListGetByRoleId(int roleId);
        Task<List<OperationClaimChildListGetByUserIdResponseDto>> OperationClaimChildListGetByRoleId(int roleId);
        Task<List<OperationClaimParentListGetByMenuIdResponseDto>> OperationClaimParentListGetByMenuId(int menuId);
        Task<List<OperationClaimChildListGetByMenuIdResponseDto>> OperationClaimChildListGetByMenuId(int menuId);
    }
}
