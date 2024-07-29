using Business.Abstract;
using Entities.Concrete.Dtos.WebLogDtos.WebLogAddDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebLogsController : ControllerBase
    {
        private IWebLogService _webLogService;
        public WebLogsController(IWebLogService webLogService)
        {
            _webLogService = webLogService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _webLogService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _webLogService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(WebLogAddRequestDto webLogAddRequestDto)
        {
            var result = await _webLogService.Add(webLogAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(WebLogUpdateRequestDto webLogUpdateRequestDto)
        {
            var result = await _webLogService.Update(webLogUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search(WebLogSearchRequestDto webLogSearchRequestDto)
        {
            var dataResult = await _webLogService.Search(webLogSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


    }
}
