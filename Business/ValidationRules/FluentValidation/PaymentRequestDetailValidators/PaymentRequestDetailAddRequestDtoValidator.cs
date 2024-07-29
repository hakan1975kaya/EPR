using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.PaymentRequestDetailValidators
{
    public class PaymentRequestDetailAddRequestDtoValidator : AbstractValidator<PaymentRequestDetailAddRequestDto>
    {
        public PaymentRequestDetailAddRequestDtoValidator()
        {
            RuleFor(x => x.ReferenceNumber).NotNull();
            RuleFor(x => x.ReferenceNumber).Length(3, 50);

            RuleFor(x => x.AccountNumber).NotNull();
            RuleFor(x => x.AccountNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CustomerNumber).NotNull();
            RuleFor(x => x.CustomerNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.PhoneNumber).NotNull();
            RuleFor(x => x.PhoneNumber).Length(3, 50);

            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.FirstName).Length(3, 50);

            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.LastName).Length(3, 50);

            RuleFor(x => x.PaymentAmount).NotNull();
            RuleFor(x => x.PaymentAmount).InclusiveBetween(decimal.MinValue, decimal.MaxValue);

            RuleFor(x => x.CardDepositDate).NotNull();
            RuleFor(x => x.CardDepositDate).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.Explanation).NotNull();
            RuleFor(x => x.Explanation).Length(3, 500);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
