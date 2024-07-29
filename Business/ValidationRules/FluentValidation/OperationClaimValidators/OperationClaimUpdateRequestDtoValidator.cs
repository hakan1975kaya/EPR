using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.OperationClaimValidators
{
    public class OperationClaimUpdateRequestDtoValidator : AbstractValidator<OperationClaimUpdateRequestDto>
    {
        public OperationClaimUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).Length(3, 50);
        }
    }
}