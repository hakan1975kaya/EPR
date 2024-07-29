using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressAddDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class CorporateMailAddressAddRequestDtoValidator : AbstractValidator<CorporateMailAddressAddRequestDto>
    {
        public CorporateMailAddressAddRequestDtoValidator()
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

