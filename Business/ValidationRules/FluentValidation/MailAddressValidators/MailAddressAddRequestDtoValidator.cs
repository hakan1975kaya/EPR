using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateMailAddressValidators
{
    public class MailAddressAddRequestDtoValidator : AbstractValidator<MailAddressAddRequestDto>
    {
        public MailAddressAddRequestDtoValidator()
        {

            RuleFor(x => x.Address).NotNull();
            RuleFor(x => x.Address).Length(2, int.MaxValue);

            RuleFor(x => x.IsCC).NotNull();
            RuleFor(x => x.IsCC).InclusiveBetween(false, true);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}

