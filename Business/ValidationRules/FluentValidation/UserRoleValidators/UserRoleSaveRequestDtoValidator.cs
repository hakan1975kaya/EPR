using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSaveDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserRoleValidators
{
    public class UserRoleSaveRequestDtoValidator : AbstractValidator<UserRoleSaveRequestDto>
    {
        public UserRoleSaveRequestDtoValidator()
        {

            RuleFor(x => x.UserRole .UserId).NotNull();
            RuleFor(x => x.UserRole.UserId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.UserRole.RoleId).NotNull();
            RuleFor(x => x.UserRole.RoleId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.UserRole.Optime).NotNull();
            RuleFor(x => x.UserRole.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.UserRole.IsActive).NotNull();
            RuleFor(x => x.UserRole.IsActive).InclusiveBetween(false, true);

        }
    }
}