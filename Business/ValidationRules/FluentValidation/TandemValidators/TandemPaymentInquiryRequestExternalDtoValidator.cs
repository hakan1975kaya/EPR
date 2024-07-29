using FluentValidation;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos;

namespace Business.ValidationRules.FluentValidation.TandemValidators
{
    public class TandemPaymentInquiryRequestExternalDtoValidator : AbstractValidator<TandemPaymentInquiryRequestExternalDto>
    {
        public TandemPaymentInquiryRequestExternalDtoValidator()
        {

            RuleFor(x => x.CorporateCode).NotNull();
            RuleFor(x => x.CorporateCode).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}