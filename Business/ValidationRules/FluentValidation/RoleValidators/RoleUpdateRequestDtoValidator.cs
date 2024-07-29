using Entities.Concrete.Dtos.RoleDtos.RoleUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.RoleDetailValidators
{
    public class RoleUpdateRequestDtoValidator : AbstractValidator<RoleUpdateRequestDto>
    {
        public RoleUpdateRequestDtoValidator()
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
