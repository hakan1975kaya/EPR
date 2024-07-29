using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Mail.Smtp.Abstract;
using Core.Utilities.Mail.Smtp.Constants;
using Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Mail.Smtp.Concrete
{
    public class SmtpHelper : ISmtpHelper
    {
        private IConfiguration _configuration;
        private SmtpInfo _smtpInfo;
        public SmtpHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpInfo = _configuration.GetSection("SmtpInfo").Get<SmtpInfo>();
        }
        public async Task<IResult> SendSmtpMail(SmtpSendRequestDto smtpSendRequestDto)
        {
            var toMailAddresses = smtpSendRequestDto.ToMailAddresses;
            var toCCMailAddresses = smtpSendRequestDto.ToCCMailAddresses;
            var body = smtpSendRequestDto.Body;

            SmtpClient smtpClient = new SmtpClient(_smtpInfo.SmtpAddress, 25);

            smtpClient.Credentials = new System.Net.NetworkCredential(_smtpInfo.SmtpMailAddres, "");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            MailMessage mail = new MailMessage();

            mail.IsBodyHtml = true;
            mail.Body = body;
            mail.From = new MailAddress(_smtpInfo.SmtpMailAddres, _smtpInfo.SmtpSiteAddress);

            if (toMailAddresses != null)
            {
                if (toMailAddresses.Count > 0)
                {
                    foreach (var toMailAddresse in toMailAddresses)
                    {
                        if (!string.IsNullOrEmpty(toMailAddresse))
                        {
                            mail.To.Add(new MailAddress(toMailAddresse));
                        }
                    }
                }
            }

            if (toCCMailAddresses != null)
            {
                if (toCCMailAddresses.Count > 0)
                {
                    foreach (var toCCMailAddresse in toCCMailAddresses)
                    {
                        if (!string.IsNullOrEmpty(toCCMailAddresse))
                        {
                            mail.CC.Add(new MailAddress(toCCMailAddresse));
                        }
                    }
                }
            }

            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception)
            {    
            }
            return new SuccessResult(SmtpMessages.SentMailSuccess);
        }
    }
}
