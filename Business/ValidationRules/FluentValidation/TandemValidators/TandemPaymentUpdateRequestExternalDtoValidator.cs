using FluentValidation;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos;

namespace Business.ValidationRules.FluentValidation.TandemValidators
{
    public class TandemPaymentUpdateRequestExternalDtoValidator : AbstractValidator<TandemPaymentUpdateRequestExternalDto>
    {
        public TandemPaymentUpdateRequestExternalDtoValidator()
        {
            RuleFor(x => x.RequestListExternals).NotNull();
        }
    }
}