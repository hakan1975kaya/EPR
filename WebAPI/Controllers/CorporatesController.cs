using Business.Abstract;
using Entities.Concrete.Dtos.CorporateDtos.CorporateAddDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateUpdateDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSaveDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporatesController : ControllerBase
    {
        private ICorporateService _corporateService;
        public CorporatesController(ICorporateService corporateService)
        {
            _corporateService = corporateService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _corporateService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _corporateService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CorporateAddRequestDto corporateAddRequestDto)
        {
            var result = await _corporateService.Add(corporateAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CorporateUpdateRequestDto corporateUpdateRequestDto)
        {
            var result = await _corporateService.Update(corporateUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _corporateService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search(CorporateSearchRequestDto corporateSearchRequestDto)
        {
            var dataResult = await _corporateService.Search(corporateSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(CorporateSaveRequestDto corporateSaveRequestDto)
        {
            var result = await _corporateService.Save(corporateSaveRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getListPrefixAvailable")]
        public async Task<IActionResult> GetListPrefixAvailable()
        {
            var dataResult = await _corporateService.GetListPrefixAvailable();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getByCorporateCode")]
        public async Task<IActionResult> GetByCorporateCode(int corporateCode)
        {
            var dataResult = await _corporateService.GetByCorporateCode(corporateCode);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


    }
}
