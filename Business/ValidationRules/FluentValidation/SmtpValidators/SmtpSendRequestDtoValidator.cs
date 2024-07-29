using Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos;
using DocumentFormat.OpenXml.Office2010.Excel;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.SmtpValidators
{
    public class SmtpSendRequestDtoValidator : AbstractValidator<SmtpSendRequestDto>
    {
        public SmtpSendRequestDtoValidator()
        {
            RuleFor(x => x.Body).NotNull();
            RuleFor(x => x.Body).Length(3, int.MaxValue);
        }
    }

}