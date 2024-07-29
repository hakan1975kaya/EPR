using Entities.Concrete.Dtos.UserRoleDtos.UserRoleUpdateDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserRoleValidators
{
    public class UserRoleUpdateRequestDtoValidator : AbstractValidator<UserRoleUpdateRequestDto>
    {
        public UserRoleUpdateRequestDtoValidator()
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