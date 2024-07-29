using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSearchDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.CorporateValidators
{
    public class OperationClaimSearchRequestDtoValidator : AbstractValidator<OperationClaimSearchRequestDto>
    {
        public OperationClaimSearchRequestDtoValidator()
        {
            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(1, int.MaxValue);

        }
    }
}
