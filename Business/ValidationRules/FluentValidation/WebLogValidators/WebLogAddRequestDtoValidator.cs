using Entities.Concrete.Dtos.WebLogDtos.WebLogAddDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.WebLogValidators
{
    public class WebLogAddRequestDtoValidator : AbstractValidator<WebLogAddRequestDto>
    {
        public WebLogAddRequestDtoValidator()
        {
            RuleFor(x => x.Detail).NotNull();
            RuleFor(x => x.Detail).Length(3, int.MaxValue);

            RuleFor(x => x.Date).NotNull();
            RuleFor(x => x.Date).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.Audit).NotNull();
            RuleFor(x => x.Audit).IsInEnum();

        }
    }
}
