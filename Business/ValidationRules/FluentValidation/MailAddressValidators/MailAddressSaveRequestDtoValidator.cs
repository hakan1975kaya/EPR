using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class MailAddressSaveRequestDtoValidator : AbstractValidator<MailAddressSaveRequestDto>
    {
        public MailAddressSaveRequestDtoValidator()
        {
            RuleFor(x => x.MailAddress.Address).NotNull();
            RuleFor(x => x.MailAddress.Address).Length(2, int.MaxValue);

            RuleFor(x => x.MailAddress.IsCC).NotNull();
            RuleFor(x => x.MailAddress.IsCC).InclusiveBetween(false, true);

            RuleFor(x => x.MailAddress.Optime).NotNull();
            RuleFor(x => x.MailAddress.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.MailAddress.IsActive).NotNull();
            RuleFor(x => x.MailAddress.IsActive).InclusiveBetween(false, true);

            RuleFor(x => x.SaveType).NotNull();
            RuleFor(x => x.SaveType).IsInEnum() ;

        }
    }
}