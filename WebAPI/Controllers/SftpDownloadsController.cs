using Business.Abstract;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadUpdateDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SftpDownloadsController : ControllerBase
    {
        private ISftpDownloadService _sftpDownloadService;
        public SftpDownloadsController(ISftpDownloadService sftpDownloadService)
        {
            _sftpDownloadService = sftpDownloadService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _sftpDownloadService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _sftpDownloadService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(SftpDownloadAddRequestDto sftpDownloadAddRequestDto)
        {
            var result = await _sftpDownloadService.Add(sftpDownloadAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(SftpDownloadUpdateRequestDto sftpDownloadUpdateRequestDto)
        {
            var result = await _sftpDownloadService.Update(sftpDownloadUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SftpDownloadSearchRequestDto sftpDownloadSearchRequestDto)
        {
            var dataResult = await _sftpDownloadService.Search(sftpDownloadSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getBySftpFileName")]
        public async Task<IActionResult> GetBySftpFileName(string sftpFileName)
        {
            var dataResult = await _sftpDownloadService.GetBySftpFileName(sftpFileName);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("openToPaymentRequest")]
        public async Task<IActionResult> OpenToPaymentRequest(int paymentRequestId)
        {
            var result = await _sftpDownloadService.OpenToPaymentRequest(paymentRequestId);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
