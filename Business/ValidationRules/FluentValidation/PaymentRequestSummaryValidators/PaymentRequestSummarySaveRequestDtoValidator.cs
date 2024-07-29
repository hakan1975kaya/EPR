using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAddDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySaveDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.PaymentRequestSummaryValidators
{
    public class PaymentRequestSummarySaveRequestDtoValidator : AbstractValidator<PaymentRequestSummarySaveRequestDto>
    {
        public PaymentRequestSummarySaveRequestDtoValidator()
        {

            RuleFor(x => x.PaymentRequestSummaries).NotNull();

            RuleFor(x => x.SaveType).NotNull();
            RuleFor(x => x.SaveType).IsInEnum();

        }
    }
}
