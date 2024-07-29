using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.RoleOperationClaimValidators
{
    public class RoleOperationClaimAddRequestDtoValidator : AbstractValidator<RoleOperationClaimAddRequestDto>
    {
        public RoleOperationClaimAddRequestDtoValidator()
        {

            RuleFor(x => x.RoleId).NotNull();
            RuleFor(x => x.RoleId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.OperationClaimId).NotNull();
            RuleFor(x => x.OperationClaimId).InclusiveBetween(int.MinValue, int.MaxValue);
        }
    }
}