using DocumentFormat.OpenXml.Office2010.Excel;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.SftpDownloadValidators
{
    public class SftpDownloadAddRequestDtoValidator : AbstractValidator<SftpDownloadAddRequestDto>
    {
        public SftpDownloadAddRequestDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.CorporateId).NotNull();
            RuleFor(x => x.CorporateId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.SftpFileName).NotNull();
            RuleFor(x => x.SftpFileName).Length(2, int.MaxValue);

            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.Status).IsInEnum();

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
