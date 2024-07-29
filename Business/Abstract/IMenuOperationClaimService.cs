using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetListDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimSaveDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimUpdateDtos;

namespace Business.Abstract
{
    public interface IMenuOperationClaimService
    {
        Task<IDataResult<MenuOperationClaimGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<MenuOperationClaimGetListResponseDto>>> GetList();
        Task<IResult> Add(MenuOperationClaimAddRequestDto menuOperationClaimAddRequestDto);
        Task<IResult> Update(MenuOperationClaimUpdateRequestDto menuOperationClaimUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IResult> Save(MenuOperationClaimSaveRequestDto menuOperationClaimSaveRequestDto);

    }
}
