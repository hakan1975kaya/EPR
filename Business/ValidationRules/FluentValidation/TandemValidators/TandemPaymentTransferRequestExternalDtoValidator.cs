using FluentValidation;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;

namespace Business.ValidationRules.FluentValidation.TandemValidators
{
    public class TandemPaymentTransferRequestExternalDtoValidator : AbstractValidator<TandemPaymentTransferRequestExternalDto>
    {
        public TandemPaymentTransferRequestExternalDtoValidator()
        {
            RuleFor(x => x.RequestNumber).NotNull();
            RuleFor(x => x.RequestNumber).Length(3, 50);

            RuleFor(x => x.CorporateCode).NotNull();
            RuleFor(x => x.CorporateCode).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.RegistrationNumber).NotNull();
            RuleFor(x => x.RegistrationNumber).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.RequestNumber).NotNull();
            RuleFor(x => x.RequestNumber).Length(3, 50);

            RuleFor(x => x.MoneyType).NotNull();
            RuleFor(x => x.MoneyType).IsInEnum();

            RuleFor(x => x.PaymentExternals).NotNull();
        }
    }
}
