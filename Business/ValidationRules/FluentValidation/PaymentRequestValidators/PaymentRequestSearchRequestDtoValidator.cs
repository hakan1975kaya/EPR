using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.MenuValidators
{
    public class PaymentRequestSearchRequestDtoValidator : AbstractValidator<PaymentRequestSearchRequestDto>
    {
        public PaymentRequestSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
