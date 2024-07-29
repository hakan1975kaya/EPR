using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimAddDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.OperationClaimValidators
{
    public class OperationClaimSaveRequestDtoValidator : AbstractValidator<OperationClaimSaveRequestDto>
    {
        public OperationClaimSaveRequestDtoValidator()
        {
            RuleFor(x => x.OperationClaim.Name).NotNull();
            RuleFor(x => x.OperationClaim.Name).Length(3,50);
        }
    }
}