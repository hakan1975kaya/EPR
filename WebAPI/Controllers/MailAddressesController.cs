using Business.Abstract;
using Core.Aspects.Autofac.Performance.Dtos;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailAddressesController : ControllerBase
    {
        private IMailAddressService _mailAddressService;
        public MailAddressesController(IMailAddressService mailAddressService)
        {
            _mailAddressService = mailAddressService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _mailAddressService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _mailAddressService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MailAddressAddRequestDto mailAddressAddRequestDto)
        {
            var result = await _mailAddressService.Add(mailAddressAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(MailAddressUpdateRequestDto mailAddressUpdateRequestDto)
        {
            var result = await _mailAddressService.Update(mailAddressUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mailAddressService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(MailAddressSearchRequestDto mailAddressSearchRequestDto)
        {
            var dataResult = await _mailAddressService.Search(mailAddressSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


        [HttpPost("save")]
        public async Task<IActionResult> Save(MailAddressSaveRequestDto mailAddressSaveRequestDto)
        {
            var result = await _mailAddressService.Save(mailAddressSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getListPtt")]
        public async Task<IActionResult>  GetListPtt()
        {
            var dataResult = await _mailAddressService.GetListPtt();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getListNotPtt")]
        public async Task<IActionResult> GetListNotPtt()
        {
            var dataResult = await _mailAddressService.GetListNotPtt();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }



    }
}
