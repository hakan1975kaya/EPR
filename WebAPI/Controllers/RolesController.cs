using Business.Abstract;
using Entities.Concrete.Dtos.RoleDtos.RoleAddDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleUpdateDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.Dtos.RoleDtos.RoleSaveDtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _roleService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _roleService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(RoleAddRequestDto roleAddRequestDto)
        {
            var result = await _roleService.Add(roleAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(RoleUpdateRequestDto roleUpdateRequestDto)
        {
            var result = await _roleService.Update(roleUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(RoleSearchRequestDto roleSearchRequestDto)
        {
            var dataResult = await _roleService.Search(roleSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(RoleSaveRequestDto roleSaveRequestDto)
        {
            var dataResult = await _roleService.Save(roleSaveRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

    }
}
