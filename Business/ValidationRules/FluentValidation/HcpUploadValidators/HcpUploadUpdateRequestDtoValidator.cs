using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadUpdateDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.HcpUploadValidators
{
    public class HcpUploadUpdateRequestDtoValidator : AbstractValidator<HcpUploadUpdateRequestDto>
    {
        public HcpUploadUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.PaymentRequestId).NotNull();
            RuleFor(x => x.PaymentRequestId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.HcpId).NotNull();

            RuleFor(x => x.Extension).NotNull();
            RuleFor(x => x.Extension).Length(int.MinValue, int.MaxValue);

            RuleFor(x => x.Explanation).NotNull();
            RuleFor(x => x.Explanation).Length(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }


    }
}
