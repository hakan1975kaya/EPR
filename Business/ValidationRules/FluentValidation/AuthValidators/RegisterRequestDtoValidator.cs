using Entities.Concrete.Dtos.AuthDtos.RegisterDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AuthValidators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.RegistrationNumber).NotNull();
            RuleFor(x => x.RegistrationNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.FirstName).Length(3, 50);

            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.LastName).Length(3, 50);


            RuleFor(x => x.Password).NotNull();
            
        }

    }
}
