using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSaveDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class CorporateMailAddressSaveRequestDtoValidator : AbstractValidator<CorporateMailAddressSaveRequestDto>
    {
        public CorporateMailAddressSaveRequestDtoValidator()
        {

            RuleFor(x => x.CorporateMailAddress .CorporateId).NotNull();
            RuleFor(x => x.CorporateMailAddress.CorporateId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CorporateMailAddress.MailAddressId).NotNull();
            RuleFor(x => x.CorporateMailAddress.MailAddressId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CorporateMailAddress.Optime).NotNull();
            RuleFor(x => x.CorporateMailAddress.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.CorporateMailAddress.IsActive).NotNull();
            RuleFor(x => x.CorporateMailAddress.IsActive).InclusiveBetween(false, true);

        }
    }
}