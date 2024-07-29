using Business.Abstract;
using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using Entities.Concrete.Dtos.UserDtos.UserPasswordChangeDtos;
using Entities.Concrete.Dtos.UserDtos.UserSaveDtos;
using Entities.Concrete.Dtos.UserDtos.UserSearchDtos;
using Entities.Concrete.Dtos.UserDtos.UserUpdateDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _userService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _userService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserAddRequestDto userAddRequestDto)
        {
            var result = await _userService.Add(userAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserUpdateRequestDto userUpdateRequestDto)
        {
            var result = await _userService.Update(userUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search(UserSearchRequestDto userSearchRequestDto)
        {
            var dataResult = await _userService.Search(userSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(UserSaveRequestDto userSaveRequestDto)
        {
            var result = await _userService.Save(userSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("passwordChange")]
        public async Task<IActionResult> PasswordChange(PasswordChangeRequestDto passwordChangeRequestDto)
        {
            var result = await _userService.PasswordChange(passwordChangeRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
