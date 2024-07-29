using FluentValidation;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimSaveDtos;

namespace Business.ValidationRules.FluentValidation.RoleOperationClaimValidators
{
    public class RoleOperationClaimSaveRequestDtoValidator : AbstractValidator<RoleOperationClaimSaveRequestDto>
    {
        public RoleOperationClaimSaveRequestDtoValidator()
        {
            RuleFor(x => x.Role.Name).NotNull();
            RuleFor(x => x.Role.Name).Length(3, 50);

            RuleFor(x => x.Role.IsActive).NotNull();
            RuleFor(x => x.Role.IsActive).InclusiveBetween(false, true);

            RuleFor(x => x.OperationClaims).NotNull();

            RuleFor(x => x.SaveType).NotNull();
            RuleFor(x => x.SaveType).IsInEnum() ;

        }
    }
}