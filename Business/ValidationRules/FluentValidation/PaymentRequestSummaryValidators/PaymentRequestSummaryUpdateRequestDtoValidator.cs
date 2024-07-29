using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.PaymentRequestSummaryValidators
{
    public class PaymentRequestSummaryUpdateRequestDtoValidator : AbstractValidator<PaymentRequestSummaryUpdateRequestDto>
    {
        public PaymentRequestSummaryUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.Status).IsInEnum();

            RuleFor(x => x.UploadDate).NotNull();
            RuleFor(x => x.UploadDate).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.Quantity).NotNull();
            RuleFor(x => x.Quantity).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.Amount).InclusiveBetween(decimal.MinValue, decimal.MaxValue);

            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.UserId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}



