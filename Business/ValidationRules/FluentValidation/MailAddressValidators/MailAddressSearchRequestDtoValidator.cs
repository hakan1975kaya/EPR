using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class MailAddressSearchRequestDtoValidator : AbstractValidator<MailAddressSearchRequestDto>
    {
        public MailAddressSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
