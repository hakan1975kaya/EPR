using Business.Abstract;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAddDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryChartByCorporateIdYear;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySaveDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySearchDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestSummariesController : ControllerBase
    {
        private IPaymentRequestSummaryService _paymentRequestSummaryService;
        public PaymentRequestSummariesController(IPaymentRequestSummaryService paymentRequestSummaryService)
        {
            _paymentRequestSummaryService = paymentRequestSummaryService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _paymentRequestSummaryService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _paymentRequestSummaryService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PaymentRequestSummaryAddRequestDto paymentRequestSummaryAddRequestDto)
        {
            var result = await _paymentRequestSummaryService.Add(paymentRequestSummaryAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(PaymentRequestSummaryUpdateRequestDto paymentRequestSummaryUpdateRequestDto)
        {
            var result = await _paymentRequestSummaryService.Update(paymentRequestSummaryUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _paymentRequestSummaryService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getListByPaymentRequestId")]
        public async Task<IActionResult> GetListByPaymentRequestId(int paymentRequestId)
        {
            var dataResult = await _paymentRequestSummaryService.GetListByPaymentRequestId(paymentRequestId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(PaymentRequestSummarySearchRequestDto paymentRequestSummarySearchRequestDto)
        {
            var dataResult = await _paymentRequestSummaryService.Search(paymentRequestSummarySearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(PaymentRequestSummarySaveRequestDto paymentRequestSummarySaveRequestDto)
        {
            var result = await _paymentRequestSummaryService.Save(paymentRequestSummarySaveRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("amountByCorporateIdYear")]
        public async Task<IActionResult> AmountByCorporateIdYear(PaymentRequestSummaryAmountByCorporateIdYearRequestDto paymentRequestSummaryAmountByCorporateIdYearRequestDto)
        {
            var dataResult = await _paymentRequestSummaryService.AmountByCorporateIdYear(paymentRequestSummaryAmountByCorporateIdYearRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getByToday")]
        public async Task<IActionResult> GetByToday()
        {
            var dataResult = await _paymentRequestSummaryService.GetByToday();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("totalOutgoingPayment")]
        public async Task<IActionResult> TotalOutgoingPayment()
        {
            var dataResult = await _paymentRequestSummaryService.TotalOutgoingPayment();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

    }
}
