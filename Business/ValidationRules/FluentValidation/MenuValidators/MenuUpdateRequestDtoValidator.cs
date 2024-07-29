using Entities.Concrete.Dtos.MenuDtos.MenuAddDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuUpdateDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.MenuValidators
{
    public class MenuUpdateRequestDtoValidator: AbstractValidator<MenuUpdateRequestDto>
    {
        public MenuUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).Length(3, 50);

            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.Description).Length(3, 50);

            RuleFor(x => x.LinkedMenuId).NotNull();
            RuleFor(x => x.LinkedMenuId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.Description).Length(3, 50);

            RuleFor(x => x.Path).NotNull();
            RuleFor(x => x.Path).Length(3, 50);

            RuleFor(x => x.MenuOrder).NotNull();
            RuleFor(x => x.MenuOrder).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
