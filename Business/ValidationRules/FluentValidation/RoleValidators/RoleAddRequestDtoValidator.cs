using Entities.Concrete.Dtos.RoleDtos.RoleAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.RoleValidators
{
    public class RoleAddRequestDtoValidator : AbstractValidator<RoleAddRequestDto>
    {
        public RoleAddRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).Length(3, 50);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
