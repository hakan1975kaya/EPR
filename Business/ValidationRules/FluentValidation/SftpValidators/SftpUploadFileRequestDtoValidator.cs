using FluentValidation;
using Sftp.Dtos.SftpUploadFileDtos;

namespace Business.ValidationRules.FluentValidation.SftpValidators
{
    public class SftpUploadFileRequestDtoValidator : AbstractValidator<SftpUploadFileRequestDto>
    {
        public SftpUploadFileRequestDtoValidator()
        {
            RuleFor(x => x.FilePath).NotNull();
            RuleFor(x => x.FilePath).Length(2, int.MaxValue);

            RuleFor(x => x.Stream).NotNull();
        }
    }
}
