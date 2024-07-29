using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimSaveDtos
{
    public class RoleOperationClaimSaveRequestDto : IDto
    {
        public Role Role { get; set; }
        public List<OperationClaim> OperationClaims { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}
