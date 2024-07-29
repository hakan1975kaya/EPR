using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.PaymentRequestValidators
{
    public class PaymentRequestUploadRequestDtoValidator : AbstractValidator<PaymentRequestDownloadRequestDto>
    {
        public PaymentRequestUploadRequestDtoValidator()
        {
            RuleFor(x => x.SftpFileName).NotNull();
            RuleFor(x => x.SftpFileName).Length(2, int.MaxValue);
        }
    }
}
