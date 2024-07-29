using Entities.Concrete.Dtos.CorporateDtos.CorporateSaveDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateValidators
{
    public class CorporateSaveRequestDtoValidator : AbstractValidator<CorporateSaveRequestDto>
    {
        public CorporateSaveRequestDtoValidator()
        {
            RuleFor(x => x.Corporate.CorporateName).NotNull();
            RuleFor(x => x.Corporate.CorporateName).Length(3, 50);

            RuleFor(x => x.Corporate.CorporateCode).NotNull();
            RuleFor(x => x.Corporate.CorporateCode).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Corporate.CorporateAccountNo).NotNull();
            RuleFor(x => x.Corporate.CorporateAccountNo).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Corporate.MoneyType).NotNull();
            RuleFor(x => x.Corporate.MoneyType).IsInEnum();

            RuleFor(x => x.Corporate.Prefix).NotNull();
            RuleFor(x => x.Corporate.Prefix).Length(3, 30);

            RuleFor(x => x.Corporate.ComissionType).NotNull();
            RuleFor(x => x.Corporate.ComissionType).IsInEnum();

            RuleFor(x => x.Corporate.ComissionMoneyType).NotNull();
            RuleFor(x => x.Corporate.ComissionMoneyType).IsInEnum();

            RuleFor(x => x.Corporate.Comission).NotNull();
            RuleFor(x => x.Corporate.Comission).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Corporate.ComissionAccountNo).NotNull();
            RuleFor(x => x.Corporate.ComissionAccountNo).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Corporate.IsActive).NotNull();
            RuleFor(x => x.Corporate.IsActive).InclusiveBetween(false, true);

            RuleFor(x => x.Corporate.Optime).NotNull();
            RuleFor(x => x.Corporate.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.SaveType).NotNull();
            RuleFor(x => x.SaveType).IsInEnum();
        }
    }
}
