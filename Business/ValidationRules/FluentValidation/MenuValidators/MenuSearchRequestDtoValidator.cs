using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.MenuValidators
{
    public class MenuSearchRequestDtoValidator : AbstractValidator<MenuSearchRequestDto>
    {
        public MenuSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
