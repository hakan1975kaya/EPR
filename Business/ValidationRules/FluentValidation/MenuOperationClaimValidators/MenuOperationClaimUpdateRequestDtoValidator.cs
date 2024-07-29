using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimUpdateDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.MenuOperationClaimValidators
{
    public class MenuOperationClaimUpdateRequestDtoValidator: AbstractValidator<MenuOperationClaimUpdateRequestDto>
    {
        public MenuOperationClaimUpdateRequestDtoValidator()
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
