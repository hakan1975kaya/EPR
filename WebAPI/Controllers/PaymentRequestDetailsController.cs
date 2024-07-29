using Business.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestDetailsController : ControllerBase
    {
        private IPaymentRequestDetailService _paymentRequestDetailsService;
        public PaymentRequestDetailsController(IPaymentRequestDetailService paymentRequestDetailsService)
        {
            _paymentRequestDetailsService = paymentRequestDetailsService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _paymentRequestDetailsService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _paymentRequestDetailsService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PaymentRequestDetailAddRequestDto PaymentRequestDetailsAddRequestDto)
        {
            var result = await _paymentRequestDetailsService.Add(PaymentRequestDetailsAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PaymentRequestDetailUpdateRequestDto PaymentRequestDetailsUpdateRequestDto)
        {
            var result = await _paymentRequestDetailsService.Update(PaymentRequestDetailsUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _paymentRequestDetailsService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getListByPaymentRequestId")]
        public async Task<IActionResult> GetListByPaymentRequestId(int paymentRequestId)
        {
            var dataResult = await _paymentRequestDetailsService.GetListByPaymentRequestId(paymentRequestId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


    }
}
