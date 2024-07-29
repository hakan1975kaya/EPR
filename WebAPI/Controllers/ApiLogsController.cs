using Business.Abstract;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogAddDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogUpdateDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiLogsController : ControllerBase
    {
        private IApiLogService _apiLogService;
        public ApiLogsController(IApiLogService apiLogService)
        {
            _apiLogService = apiLogService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _apiLogService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _apiLogService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(ApiLogAddRequestDto ApiLogAddRequestDto)
        {
            var result = await _apiLogService.Add(ApiLogAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ApiLogUpdateRequestDto ApiLogUpdateRequestDto)
        {
            var result = await _apiLogService.Update(ApiLogUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(ApiLogSearchRequestDto apiLogSearchRequestDto)
        {
            var dataResult = await _apiLogService.Search(apiLogSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


    }
}
