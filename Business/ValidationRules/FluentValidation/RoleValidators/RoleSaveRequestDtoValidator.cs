using Entities.Concrete.Dtos.RoleDtos.RoleSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.RoleValidators
{
    public class RoleSaveRequestDtoValidator : AbstractValidator<RoleSaveRequestDto>
    {
        public RoleSaveRequestDtoValidator()
        {
            RuleFor(x => x.Role.Name).NotNull();
            RuleFor(x => x.Role.Name).Length(3, 50);

            RuleFor(x => x.Role.Optime).NotNull();
            RuleFor(x => x.Role.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.Role.IsActive).NotNull();
            RuleFor(x => x.Role.IsActive).InclusiveBetween(false, true);
        }
    }
}
