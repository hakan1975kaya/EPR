using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimSaveDtos
{
    public class MenuOperationClaimSaveRequestDto : IDto
    {
        public Menu Menu { get; set; }
        public List<OperationClaim> OperationClaims { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}
