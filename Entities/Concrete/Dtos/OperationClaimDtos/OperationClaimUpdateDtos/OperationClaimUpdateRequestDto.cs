using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimUpdateDtos
{
    public class OperationClaimUpdateRequestDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LinkedOperationClaimId { get; set; }
        public bool IsActive { get; set; }
    }
}
