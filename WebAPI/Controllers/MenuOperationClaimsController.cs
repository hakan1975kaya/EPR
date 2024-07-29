using Business.Abstract;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimSaveDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuOperationClaimsController : ControllerBase
    {
        private IMenuOperationClaimService _menuOperationClaimService;
        public MenuOperationClaimsController(IMenuOperationClaimService menuOperationClaimService)
        {
            _menuOperationClaimService = menuOperationClaimService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _menuOperationClaimService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _menuOperationClaimService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(MenuOperationClaimAddRequestDto menuOperationClaimAddRequestDto)
        {
            var result = await _menuOperationClaimService.Add(menuOperationClaimAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(MenuOperationClaimUpdateRequestDto menuOperationClaimUpdateRequestDto)
        {
            var result = await _menuOperationClaimService.Update(menuOperationClaimUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _menuOperationClaimService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(MenuOperationClaimSaveRequestDto menuOperationClaimSaveRequestDto)
        {
            var result = await _menuOperationClaimService.Save(menuOperationClaimSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }



    }
}
