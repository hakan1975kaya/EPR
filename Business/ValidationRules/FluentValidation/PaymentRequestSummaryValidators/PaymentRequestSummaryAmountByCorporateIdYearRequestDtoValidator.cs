using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAddDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryChartByCorporateIdYear;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.PaymentRequestSummaryValidators
{
    public class PaymentRequestSummaryAmountByCorporateIdYearRequestDtoValidator : AbstractValidator<PaymentRequestSummaryAmountByCorporateIdYearRequestDto>
    {
        public PaymentRequestSummaryAmountByCorporateIdYearRequestDtoValidator()
        {

            RuleFor(x => x.CorporateId).NotNull();
            RuleFor(x => x.CorporateId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Year).NotNull();
            RuleFor(x => x.Year).InclusiveBetween(int.MinValue, int.MaxValue);

        }
    }
}
