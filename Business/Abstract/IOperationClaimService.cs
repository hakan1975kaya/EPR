using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimAddDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSaveDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSearchDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimUpdateDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByMenuIdResponseDtos;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<IDataResult<OperationClaimGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<OperationClaimGetListResponseDto>>> GetList();
        Task<IResult> Add(OperationClaimAddRequestDto operationClaimAddRequestDto);
        Task<IResult> Update(OperationClaimUpdateRequestDto operationClaimUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<OperationClaimParentListResponseDto>>> OperationClaimParentList();
        Task<IDataResult<List<OperationClaimChildListResponseDto>>> OperationClaimChildList();
        Task<IDataResult<List<OperationClaimParentListGetByUserIdResponseDto>>> OperationClaimParentListGetByUserId(int userId);
        Task<IDataResult<List<OperationClaimChildListGetByUserIdResponseDto>>> OperationClaimChildListGetByUserId(int userId);
        Task<IDataResult<List<OperationClaimParentListGetByUserIdResponseDto>>> OperationClaimParentListGetByRoleId(int roleId);
        Task<IDataResult<List<OperationClaimChildListGetByUserIdResponseDto>>> OperationClaimChildListGetByRoleId(int roleId);
        Task<IDataResult<List<OperationClaimParentListGetByMenuIdResponseDto>>> OperationClaimParentListGetByMenuId(int menuId);
        Task<IDataResult<List<OperationClaimChildListGetByMenuIdResponseDto>>> OperationClaimChildListGetByMenuId(int menuId);
        Task<IDataResult<List<OperationClaimSearchResponseDto>>> Search(OperationClaimSearchRequestDto operationClaimSearchRequestDto);
        Task<IResult> Save(OperationClaimSaveRequestDto operationClaimSaveRequestDto);
    }
}
