using Business.Abstract;
using Entities.Concrete.Dtos.AuthDtos.LoginDtos;
using Entities.Concrete.Dtos.AuthDtos.RegisterDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : Controller
    {
        private IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var dataResult = await _authService.Login(loginRequestDto);
            if (dataResult.Success)
            {
                if (dataResult.Data != null)
                {
                    return Ok(dataResult);
                }
            }
            return BadRequest(dataResult);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var dataResult = await _authService.Register(registerRequestDto);
            if (dataResult.Success)
            {
                if (dataResult.Data != null)
                {
                    return Ok(dataResult);
                }
            }
            return BadRequest(dataResult);
        }

        [HttpGet("userGetByRegistrationNumber")]
        public async Task<IActionResult> UserGetRegistrationNumberName(int registrationNumber)
        {
            var dataResult = await _authService.UserGetByRegistrationNumber(registrationNumber);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("operationClaimGetListByUserId")]
        public async Task<IActionResult> OperationClaimGetListByUserId(int userId)
        {
            var dataResult = await _authService.OperationClaimGetListByUserId(userId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("menuListGetByUserId")]
        public async Task<IActionResult> MenuListGetByUserId(int userId)
        {
            var dataResult = await _authService.MenuListGetByUserId(userId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }






    }
}
