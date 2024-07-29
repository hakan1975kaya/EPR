using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.MenuOperationClaimValidators
{
    public class MenuOperationClaimAddRequestDtoValidator : AbstractValidator<MenuOperationClaimAddRequestDto>
    {
        public MenuOperationClaimAddRequestDtoValidator()
        {
            RuleFor(x => x.MenuId).NotNull();
            RuleFor(x => x.MenuId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.OperationClaimId).NotNull();
            RuleFor(x => x.OperationClaimId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
