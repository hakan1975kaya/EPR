using Business.Abstract;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressAddDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSaveDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateMailAddressesController : ControllerBase
    {
        private ICorporateMailAddressService _corporateMailAddressService;
        public CorporateMailAddressesController(ICorporateMailAddressService corporateMailAddressService)
        {
            _corporateMailAddressService = corporateMailAddressService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _corporateMailAddressService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _corporateMailAddressService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CorporateMailAddressAddRequestDto corporateMailAddressAddRequestDto)
        {
            var result = await _corporateMailAddressService.Add(corporateMailAddressAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CorporateMailAddressUpdateRequestDto corporateMailAddressUpdateRequestDto)
        {
            var result = await _corporateMailAddressService.Update(corporateMailAddressUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _corporateMailAddressService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(CorporateMailAddressSearchRequestDto corporateMailAddressSearchRequestDto)
        {
            var dataResult = await _corporateMailAddressService.Search(corporateMailAddressSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


        [HttpPost("save")]
        public async Task<IActionResult> Save(CorporateMailAddressSaveRequestDto corporateMailAddressSaveRequestDto)
        {
            var result = await _corporateMailAddressService.Save(corporateMailAddressSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByCorporateId")]
        public async Task<IActionResult> GetByCorporateId(int corporateId)
        {
            var dataResult = await _corporateMailAddressService.GetByCorporateId(corporateId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }




    }
}
