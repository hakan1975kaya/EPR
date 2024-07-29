using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSearchDtos
{
    public class OperationClaimSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}
