using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class CorporateMailAddressSearchRequestDtoValidator : AbstractValidator<CorporateMailAddressSearchRequestDto>
    {
        public CorporateMailAddressSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
