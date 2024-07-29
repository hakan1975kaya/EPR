using Business.Abstract;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadAddDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadSearchDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HcpUploadsController : ControllerBase
    {
        private IHcpUploadService _hcpUploadService;
        public HcpUploadsController(IHcpUploadService hcpUploadService)
        {
            _hcpUploadService = hcpUploadService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _hcpUploadService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _hcpUploadService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(HcpUploadAddRequestDto hcpUploadAddRequestDto)
        {
            var result = await _hcpUploadService.Add(hcpUploadAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(HcpUploadUpdateRequestDto hcpUploadUpdateRequestDto)
        {
            var result = await _hcpUploadService.Update(hcpUploadUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(HcpUploadSearchRequestDto hcpUploadSearchRequestDto)
        {
            var dataResult = await _hcpUploadService.Search(hcpUploadSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


    }
}
