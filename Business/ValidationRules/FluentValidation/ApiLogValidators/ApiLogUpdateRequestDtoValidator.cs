using Entities.Concrete.Dtos.ApiLogDtos.ApiLogAddDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogUpdateDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.ApiLogValidators
{
    public class ApiLogUpdateRequestDtoValidator : AbstractValidator<ApiLogUpdateRequestDto>
    {
        public ApiLogUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Audit).NotNull();
            RuleFor(x => x.Audit).IsInEnum();

            RuleFor(x => x.Detail).NotNull();
            RuleFor(x => x.Detail).Length(3, int.MaxValue);

            RuleFor(x => x.Date).NotNull();
            RuleFor(x => x.Date).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);
        }


    }
}

