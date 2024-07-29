using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Dtos.SmtpDtos.SmtpErrorMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpSuccessMailDtos;

namespace Entities.Concrete.Dtos.SmtpDtos.SmtpCorporateMailDtos
{
    public class SmtpCorporateMailResponseDto
    {
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public List<SmtpSuccessMailResponseDto> SmtpSuccessMailResponseDtos { get; set; }
        public List<SmtpErrorMailResponseDto> SmtpErrorMailResponseDtos { get; set; }
    }
}
