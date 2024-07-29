using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.SmtpDtos.SmtpStopMailDtos
{
    public class SmtpStopMailResponseDto
    {
        public DateTime StopDate { get; set; }
        public double ProcessingTimeSeconds { get; set; }
    }
}
