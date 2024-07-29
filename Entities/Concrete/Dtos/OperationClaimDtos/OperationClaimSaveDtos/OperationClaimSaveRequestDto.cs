using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSaveDtos
{
    public class OperationClaimSaveRequestDto : IDto
    {
        public OperationClaim OperationClaim { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}
