using Entities.Concrete.Dtos.AuthDtos.LoginDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AuthValidators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.RegistrationNumber).NotNull();
            RuleFor(x => x.RegistrationNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Password).NotNull();

        }
      
    }
}
