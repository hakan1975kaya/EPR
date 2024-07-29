using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.SftpDownloadValidators
{
    public class SftpDownloadSearchRequestDtoValidator : AbstractValidator<SftpDownloadSearchRequestDto>
    {
        public SftpDownloadSearchRequestDtoValidator()
        {

            RuleFor(x => x.Filter).NotNull();
            RuleFor(x => x.Filter).Length(3, int.MaxValue);
        }
    }
}
