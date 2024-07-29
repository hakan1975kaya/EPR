using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserRoleValidators
{
    public class UserRoleSearchRequestDtoValidator : AbstractValidator<UserRoleSearchRequestDto>
    {
        public UserRoleSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
