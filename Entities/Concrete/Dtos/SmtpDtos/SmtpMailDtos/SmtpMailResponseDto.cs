using Entities.Concrete.Dtos.SmtpDtos.SmtpErrorMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpStartMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpStopMailDtos;
using Entities.Concrete.Dtos.SmtpDtos.SmtpSuccessMailDtos;

namespace Entities.Concrete.Dtos.SmtpDtos.SmtpMailDtos
{
    public class SmtpMailResponseDto
    {
        public SmtpStartMailResponseDto SmtpStartMailResponseDtos{ get; set; }
        public List<SmtpSuccessMailResponseDto> SmtpSuccessMailResponseDtos { get; set; }
        public List<SmtpErrorMailResponseDto> SmtpErrorMailResponseDtos { get; set; }
        public SmtpStopMailResponseDto SmtpStopMailResponseDto { get; set; }
    }
}
