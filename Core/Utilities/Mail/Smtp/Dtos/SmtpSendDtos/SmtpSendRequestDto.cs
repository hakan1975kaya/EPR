using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos
{
    public class SmtpSendRequestDto
    {
        public List< string> ToMailAddresses { get; set; }
        public List<string> ToCCMailAddresses { get; set; }
        public string Body { get; set; }
    }
}
