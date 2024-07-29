using Entities.Concrete.Dtos.AuthDtos.AccessTokenDtoS;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AuthValidators
{
    public class AccessTokenAddRequestDtoValidator : AbstractValidator<AccessTokenAddRequestDto>
    {
        public AccessTokenAddRequestDtoValidator()
        {

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);

        }
    }
}

