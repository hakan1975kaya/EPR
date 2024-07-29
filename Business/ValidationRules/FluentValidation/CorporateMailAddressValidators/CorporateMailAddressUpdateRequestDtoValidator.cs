using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressUpdateDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class CorporateMailAddressUpdateRequestDtoValidator : AbstractValidator<CorporateMailAddressUpdateRequestDto>
    {
        public CorporateMailAddressUpdateRequestDtoValidator()
        {

            RuleFor(x => x.CorporateId).NotNull();
            RuleFor(x => x.CorporateId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.MailAddressId).NotNull();
            RuleFor(x => x.MailAddressId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}