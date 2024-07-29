using Business.Abstract;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimAddDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimUpdateDtos;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimSaveDtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleOperationClaimsController : ControllerBase
    {
        private IRoleOperationClaimService _roleOperationClaimService;
        public RoleOperationClaimsController(IRoleOperationClaimService roleOperationClaimService)
        {
            _roleOperationClaimService = roleOperationClaimService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _roleOperationClaimService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _roleOperationClaimService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(RoleOperationClaimAddRequestDto roleOperationClaimAddRequestDto)
        {
            var result = await _roleOperationClaimService.Add(roleOperationClaimAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(RoleOperationClaimUpdateRequestDto roleOperationClaimUpdateRequestDto)
        {
            var result = await _roleOperationClaimService.Update(roleOperationClaimUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleOperationClaimService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(RoleOperationClaimSaveRequestDto roleOperationClaimSaveRequestDto)
        {
            var result = await _roleOperationClaimService.Save(roleOperationClaimSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }






    }
}
