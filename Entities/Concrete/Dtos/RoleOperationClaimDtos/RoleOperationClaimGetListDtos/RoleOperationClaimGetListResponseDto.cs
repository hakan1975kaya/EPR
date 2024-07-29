using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetListDtos
{
    public class RoleOperationClaimGetListResponseDto : IDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int OperationClaimId { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}
