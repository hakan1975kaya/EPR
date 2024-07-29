using Entities.Concrete.Dtos.CorporateDtos.CorporateAddDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.CorporateValidators
{
    public class CorporateAddRequestDtoValidator : AbstractValidator<CorporateAddRequestDto>
    {
        public CorporateAddRequestDtoValidator()
        {
            RuleFor(x => x.CorporateName).NotNull();
            RuleFor(x => x.CorporateName).Length(3, 50);

            RuleFor(x => x.CorporateCode).NotNull();
            RuleFor(x => x.CorporateCode).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CorporateAccountNo).NotNull();
            RuleFor(x => x.CorporateAccountNo).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.MoneyType).NotNull();
            RuleFor(x => x.MoneyType).IsInEnum();

            RuleFor(x => x.Prefix).NotNull();
            RuleFor(x => x.Prefix).Length(2, int.MaxValue);

            RuleFor(x => x.ComissionType).NotNull();
            RuleFor(x => x.ComissionType).IsInEnum();

            RuleFor(x => x.ComissionMoneyType).NotNull();
            RuleFor(x => x.ComissionMoneyType).IsInEnum();

            RuleFor(x => x.Comission).NotNull();
            RuleFor(x => x.Comission).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.ComissionAccountNo).NotNull();
            RuleFor(x => x.ComissionAccountNo).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);


        }
    }
}
