using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimAddDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetListDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimSaveDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimUpdateDtos;

namespace Business.Abstract
{
    public interface IRoleOperationClaimService
    {
        Task<IDataResult<RoleOperationClaimGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<RoleOperationClaimGetListResponseDto>>> GetList();
        Task<IResult> Add(RoleOperationClaimAddRequestDto roleOperationClaimAddRequestDto);
        Task<IResult> Update(RoleOperationClaimUpdateRequestDto roleOperationClaimUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IResult> Save(RoleOperationClaimSaveRequestDto roleOperationClaimSaveRequestDto);
    }
}
