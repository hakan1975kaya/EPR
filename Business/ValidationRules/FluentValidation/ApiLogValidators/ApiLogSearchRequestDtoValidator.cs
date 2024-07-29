using Entities.Concrete.Dtos.ApiLogDtos.ApiLogAddDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.ApiLogValidators
{
    public class ApiLogSearchRequestDtoValidator : AbstractValidator<ApiLogSearchRequestDto>
    {
        public ApiLogSearchRequestDtoValidator()
        {
            RuleFor(x => x.Audit).NotNull();
            RuleFor(x => x.Audit).IsInEnum();

            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
