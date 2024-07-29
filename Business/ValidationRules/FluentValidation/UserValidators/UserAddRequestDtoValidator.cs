using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserValidators
{
    public class UserAddRequestDtoValidator :AbstractValidator<UserAddRequestDto>
    {
     public UserAddRequestDtoValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.FirstName).Length(3, 50);

            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.LastName).Length(3, 50);

            RuleFor(x => x.RegistrationNumber).NotNull();
            RuleFor(x => x.RegistrationNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
