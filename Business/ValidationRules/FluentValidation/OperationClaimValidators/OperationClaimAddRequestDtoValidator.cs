using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.OperationClaimValidators
{
    public class OperationClaimAddRequestDtoValidator : AbstractValidator<OperationClaimAddRequestDto>
    {
        public OperationClaimAddRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).Length(3,50);
        }
    }
}