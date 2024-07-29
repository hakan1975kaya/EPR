using Business.Abstract;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimAddDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSaveDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSearchDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private IOperationClaimService _operationClaimService;
        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _operationClaimService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _operationClaimService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(OperationClaimAddRequestDto operationClaimAddRequestDto)
        {
            var result = await _operationClaimService.Add(operationClaimAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OperationClaimUpdateRequestDto operationClaimUpdateRequestDto)
        {
            var result = await _operationClaimService.Update(operationClaimUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _operationClaimService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("operationClaimParentList")]
        public async Task<IActionResult> OperationClaimParentList()
        {
            var dataResult = await _operationClaimService.OperationClaimParentList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimChildList")]
        public async Task<IActionResult> OperationClaimChildList()
        {
            var dataResult = await _operationClaimService.OperationClaimChildList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimParentListGetByUserId")]
        public async Task<IActionResult> OperationClaimParentListGetByUserId(int userId)
        {
            var dataResult = await _operationClaimService.OperationClaimParentListGetByUserId(userId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimChildListGetByUserId")]
        public async Task<IActionResult> OperationClaimChildListGetByUserId(int userId)
        {
            var dataResult = await _operationClaimService.OperationClaimChildListGetByUserId(userId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimParentListGetByRoleId")]
        public async Task<IActionResult> OperationClaimParentListGetByRoleId(int roleId)
        {
            var dataResult = await _operationClaimService.OperationClaimParentListGetByRoleId(roleId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimChildListGetByRoleId")]
        public async Task<IActionResult> OperationClaimChildListGetByRoleId(int roleId)
        {
            var dataResult = await _operationClaimService.OperationClaimChildListGetByRoleId(roleId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimParentListGetByMenuId")]
        public async Task<IActionResult> OperationClaimParentListGetByMenuId(int menuId)
        {
            var dataResult = await _operationClaimService.OperationClaimParentListGetByMenuId(menuId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimChildListGetByMenuId")]
        public async Task<IActionResult> OperationClaimChildListGetByMenuId(int menuId)
        {
            var dataResult = await _operationClaimService.OperationClaimChildListGetByMenuId(menuId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search(OperationClaimSearchRequestDto operationClaimSearchRequestDto)
        {
            var dataResult = await _operationClaimService.Search(operationClaimSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(OperationClaimSaveRequestDto operationClaimSaveRequestDto)
        {
            var result = await _operationClaimService.Save(operationClaimSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }








    }
}
