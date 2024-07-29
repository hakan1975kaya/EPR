using Business.Abstract;
using Core.Utilities.Mail.Smtp.Dtos.SmtpSendDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmtpsController : ControllerBase
    {
        private ISmtpService _smtpService;
        public SmtpsController(ISmtpService smtpService )
        {
            _smtpService = smtpService;      
        }

        [HttpPost("sendSmtpMail")]
        public async Task<IActionResult>  SendSmtpMail(SmtpSendRequestDto smtpSendRequestDto)
        {
            var result = await _smtpService.SendSmtpMail(smtpSendRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
