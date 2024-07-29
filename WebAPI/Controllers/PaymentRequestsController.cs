using Business.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSaveDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestUpdateDtos;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos;
using Core.Utilities.Results.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestsController : ControllerBase
    {
        private IPaymentRequestService _paymentRequestService;
        public PaymentRequestsController(IPaymentRequestService paymentRequestService)
        {
            _paymentRequestService = paymentRequestService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _paymentRequestService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _paymentRequestService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PaymentRequestAddRequestDto paymentRequestsAddRequestDto)
        {
            var result = await _paymentRequestService.Add(paymentRequestsAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PaymentRequestUpdateRequestDto paymentRequestsUpdateRequestDto)
        {
            var result = await _paymentRequestService.Update(paymentRequestsUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _paymentRequestService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(PaymentRequestSearchRequestDto paymentRequestSearchRequestDto)
        {
            var dataResult = await _paymentRequestService.Search(paymentRequestSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(PaymentRequestSaveRequestDto paymentRequestSaveRequestDto)
        {
            var result = await _paymentRequestService.Save(paymentRequestSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByToday")]
        public async Task<IActionResult> GetByToday()
        {
            var dataResult = await _paymentRequestService.GetByToday();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("paymentRequestDownload")]
        public async Task<IActionResult> PaymentRequestDownload(PaymentRequestDownloadRequestDto paymentRequestDownloadRequestDto)
        {
            var dataResult = await _paymentRequestService.PaymentRequestDownload(paymentRequestDownloadRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getListByCorporateId")]
        public async Task<IActionResult> GetListByCorporateId(int corporateId)
        {
            var dataResult = await _paymentRequestService.GetListByCorporateId(corporateId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

    }
}
