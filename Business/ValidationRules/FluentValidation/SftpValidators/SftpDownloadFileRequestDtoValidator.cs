using DocumentFormat.OpenXml.Office2010.Excel;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using FluentValidation;
using Sftp.Dtos.SftpDownloadFileDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.SftpValidators
{
    public class SftpDownloadFileRequestDtoValidator : AbstractValidator<SftpDownloadFileRequestDto>
    {
        public SftpDownloadFileRequestDtoValidator()
        {
            RuleFor(x => x.SftpFileName).NotNull();
            RuleFor(x => x.SftpFileName).Length(2, int.MaxValue);

            RuleFor(x => x.Prefix).NotNull();
            RuleFor(x => x.Prefix).Length(2, int.MaxValue);
        }
    }
}
