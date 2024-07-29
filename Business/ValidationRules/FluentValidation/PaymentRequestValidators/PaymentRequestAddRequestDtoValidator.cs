using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.PaymentRequestValidators
{
    public class PaymentRequestAddRequestDtoValidator : AbstractValidator<PaymentRequestAddRequestDto>
    {
        public PaymentRequestAddRequestDtoValidator()
        {
            RuleFor(x => x.RequestNumber).NotNull();
            RuleFor(x => x.RequestNumber).Length(3, 50);

            RuleFor(x => x.CorporateId).NotNull();
            RuleFor(x => x.CorporateId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.UserId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
