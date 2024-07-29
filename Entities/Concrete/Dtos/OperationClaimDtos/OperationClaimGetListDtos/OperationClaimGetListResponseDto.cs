using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos
{
    public class OperationClaimGetListResponseDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LinkedOperationClaimId { get; set; }
        public bool IsActive { get; set; }
    }
}
