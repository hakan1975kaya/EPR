using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimSaveDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.MenuOperationClaimValidators
{
    public class MenuOperationClaimSaveRequestDtoValidator : AbstractValidator<MenuOperationClaimSaveRequestDto>
    {
        public MenuOperationClaimSaveRequestDtoValidator()
        {
            RuleFor(x =>x.Menu. Name).NotNull();
            RuleFor(x =>x.Menu.Name).Length(3, 50);

            RuleFor(x =>x.Menu.Description).NotNull();
            RuleFor(x =>x.Menu.Description).Length(3, 50);

            RuleFor(x =>x.Menu.LinkedMenuId).NotNull();
            RuleFor(x =>x.Menu.LinkedMenuId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x =>x.Menu.Description).NotNull();
            RuleFor(x =>x.Menu.Description).Length(3, 50);

            RuleFor(x =>x.Menu.Path).NotNull();
            RuleFor(x =>x.Menu.Path).Length(3,50);

            RuleFor(x =>x.Menu.MenuOrder).NotNull();
            RuleFor(x =>x.Menu.MenuOrder).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x =>x.Menu.IsActive).NotNull();
            RuleFor(x =>x.Menu.IsActive).InclusiveBetween(false, true);

            RuleFor(x => x.OperationClaims).NotNull();

            RuleFor(x => x.SaveType).NotNull();
            RuleFor(x => x.SaveType).IsInEnum();
        }
    }
}
