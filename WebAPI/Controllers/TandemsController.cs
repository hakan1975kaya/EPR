using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TandemsController : ControllerBase
    {
        private ITandemService _tandemService;
        public TandemsController(ITandemService tandemService)
        {
            _tandemService = tandemService;
        }

        [HttpPost("paymentTransfer")]
        public async Task<IActionResult> PaymentTransferExternal(TandemPaymentTransferRequestExternalDto tandemPaymentTransferRequestExternalDto)
        {
            var result = await _tandemService.PaymentTransfer(tandemPaymentTransferRequestExternalDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("paymentUpdate")]
        public async Task<IActionResult> PaymentUpdate(TandemPaymentUpdateRequestExternalDto tandemPaymentUpdateRequestExternalDto)
        {
            var result = await _tandemService.PaymentUpdate(tandemPaymentUpdateRequestExternalDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("paymentInquiry")]
        public async Task<IActionResult> PaymentInquiry(TandemPaymentInquiryRequestExternalDto tandemPaymentInquiryRequestExternalDto)
        {
            var result = await _tandemService.PaymentInquiry(tandemPaymentInquiryRequestExternalDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("corporateDefine")]
        public async Task<IActionResult> CorporateDefine(TandemCorporateDefineRequestExternalDto tandemCorporateDefineRequestExternalDto)
        {
            var result = await _tandemService.CorporateDefine(tandemCorporateDefineRequestExternalDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
