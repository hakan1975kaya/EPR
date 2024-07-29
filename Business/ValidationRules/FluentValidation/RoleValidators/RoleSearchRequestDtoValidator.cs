using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.MenuValidators
{
    public class RoleSearchRequestDtoValidator : AbstractValidator<RoleSearchRequestDto>
    {
        public RoleSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
