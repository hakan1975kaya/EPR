using Entities.Concrete.Dtos.UserDtos.UserUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserValidators
{
    public class UserUpdateRequestDtoValidator : AbstractValidator<UserUpdateRequestDto>
    {
        public UserUpdateRequestDtoValidator()
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
