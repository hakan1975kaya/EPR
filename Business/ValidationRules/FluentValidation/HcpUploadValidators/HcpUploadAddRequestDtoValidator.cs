using DocumentFormat.OpenXml.Office2010.Excel;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadAddDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.HcpUploadValidators
{
    public class HcpUploadAddRequestDtoValidator : AbstractValidator<HcpUploadAddRequestDto>
    {
        public HcpUploadAddRequestDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.PaymentRequestId).NotNull();
            RuleFor(x => x.PaymentRequestId).InclusiveBetween(int.MinValue, int.MaxValue);

            RuleFor(x => x.HcpId).NotNull();

            RuleFor(x => x.Extension).NotNull();
            RuleFor(x => x.Extension).Length(int.MinValue,int.MaxValue);

            RuleFor(x => x.Explanation).NotNull();
            RuleFor(x => x.Explanation).Length(int.MinValue, int.MaxValue);

            RuleFor(x => x.Optime).NotNull();
            RuleFor(x => x.Optime).InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsActive).InclusiveBetween(false, true);
        }
    }
}
