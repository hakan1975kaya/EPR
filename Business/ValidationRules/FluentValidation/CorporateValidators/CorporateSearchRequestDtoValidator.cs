using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateValidators
{
    public class CorporateSearchRequestDtoValidator : AbstractValidator<CorporateSearchRequestDto>
    {
        public CorporateSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
