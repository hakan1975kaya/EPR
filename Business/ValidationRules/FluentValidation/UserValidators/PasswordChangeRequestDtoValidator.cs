using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using Entities.Concrete.Dtos.UserDtos.UserPasswordChangeDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserValidators
{
    public class PasswordChangeRequestDtoValidator : AbstractValidator<PasswordChangeRequestDto>
    {
     public PasswordChangeRequestDtoValidator()
        {
            RuleFor(x => x.RegistrationNumber).NotNull();
            RuleFor(x => x.RegistrationNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Password).NotNull();
        }
    }
}
