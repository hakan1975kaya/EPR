using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.SmtpDtos.SmtpErrorMailDtos
{
    public class SmtpErrorMailResponseDto
    {
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public string FileName { get; set; }
        public string Exception { get; set; }
    }
}
