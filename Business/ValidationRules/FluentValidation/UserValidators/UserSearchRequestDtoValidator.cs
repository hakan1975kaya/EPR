using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;
using Entities.Concrete.Dtos.UserDtos.UserSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.MenuValidators
{
    public class UserSearchRequestDtoValidator : AbstractValidator<UserSearchRequestDto>
    {
        public UserSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
