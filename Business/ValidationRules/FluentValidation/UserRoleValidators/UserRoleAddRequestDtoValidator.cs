using Entities.Concrete.Dtos.UserRoleDtos.UserRoleAddDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserRoleValidators
{
    public class UserRoleAddRequestDtoValidator : AbstractValidator<UserRoleAddRequestDto>
    {
        public UserRoleAddRequestDtoValidator()
        {

            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.UserId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.RoleId).NotNull();
            RuleFor(x => x.RoleId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}

