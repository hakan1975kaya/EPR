using Business.Abstract;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleAddDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSaveDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private IUserRoleService _userRoleService;
        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _userRoleService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _userRoleService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserRoleAddRequestDto userRoleAddRequestDto)
        {
            var result = await _userRoleService.Add(userRoleAddRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserRoleUpdateRequestDto userRoleUpdateRequestDto)
        {
            var result = await _userRoleService.Update(userRoleUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userRoleService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(UserRoleSearchRequestDto userRoleSearchRequestDto)
        {
            var dataResult = await _userRoleService.Search(userRoleSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }


        [HttpPost("save")]
        public async Task<IActionResult> Save(UserRoleSaveRequestDto userRoleSaveRequestDto)
        {
            var result = await _userRoleService.Save(userRoleSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByRoleId")]
        public async Task<IActionResult> GetByRoleId(int roleId)
        {
            var dataResult = await _userRoleService.GetByRoleId(roleId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }




    }
}
