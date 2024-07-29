using Entities.Concrete.Enums;
using FluentValidation;
using Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos;

namespace Business.ValidationRules.FluentValidation.TandemValidators
{
    public class TandemCorporateDefineRequestExternalDtoValidator : AbstractValidator<TandemCorporateDefineRequestExternalDto>
    {
        public TandemCorporateDefineRequestExternalDtoValidator()
        {
            RuleFor(x => x.CorporateExternal.CorporateCode).NotNull();
            RuleFor(x => x.CorporateExternal.CorporateCode).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CorporateExternal.CorporateName).NotNull();
            RuleFor(x => x.CorporateExternal.CorporateName).Length(3, 50);

            RuleFor(x => x.CorporateExternal.CorporateAccountNo).NotNull();
            RuleFor(x => x.CorporateExternal.CorporateAccountNo).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CorporateExternal.MoneyType).NotNull();
            RuleFor(x => x.CorporateExternal.MoneyType).IsInEnum();

            RuleFor(x => x.CorporateExternal.Prefix).NotNull();
            RuleFor(x => x.CorporateExternal.Prefix).Length(3, 50);
        }
    }
}
