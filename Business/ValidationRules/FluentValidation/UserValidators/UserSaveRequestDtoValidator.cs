using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using Entities.Concrete.Dtos.UserDtos.UserSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserValidators
{
    public class UserSaveRequestDtoValidator : AbstractValidator<UserSaveRequestDto>
    {
     public UserSaveRequestDtoValidator()
        {
            RuleFor(x => x.User.FirstName).NotNull();
            RuleFor(x => x.User.FirstName).Length(3, 50);

            RuleFor(x => x.User.LastName).NotNull();
            RuleFor(x => x.User.LastName).Length(3, 50);

            RuleFor(x => x.User.RegistrationNumber).NotNull();
            RuleFor(x => x.User.RegistrationNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.User.IsActive).NotNull();
            RuleFor(x => x.User.IsActive).InclusiveBetween(false, true);
        }
    }
}
