using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.PaymentRequestValidators
{
    public class PaymentRequestSaveRequestDtoValidator : AbstractValidator<PaymentRequestSaveRequestDto>
    {
        public PaymentRequestSaveRequestDtoValidator()
        {
            RuleFor(x => x.PaymentRequest. RequestNumber).NotNull();
            RuleFor(x => x.PaymentRequest.RequestNumber).Length(3, 50);

            RuleFor(x => x.PaymentRequest.CorporateId).NotNull();
            RuleFor(x => x.PaymentRequest.CorporateId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.PaymentRequest.UserId).NotNull();
            RuleFor(x => x.PaymentRequest.UserId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.PaymentRequest.IsActive).NotNull();
            RuleFor(x => x.PaymentRequest.IsActive).InclusiveBetween(false, true);

            RuleFor(x => x.PaymentRequestDetails).NotNull();

            RuleFor(x => x.PaymentRequest.Optime).NotNull();
            RuleFor(x => x.PaymentRequest.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.SaveType).NotNull();
            RuleFor(x => x.SaveType).IsInEnum();
        }
    }
}
