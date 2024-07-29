using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos;
using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Mail.Smtp.Abstract
{
    public interface ISmtpHelper
    {
        Task<IResult> SendSmtpMail(SmtpSendRequestDto smtpSendRequestDto);
    }
}
