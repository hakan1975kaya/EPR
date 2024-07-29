using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindowsController : ControllerBase
    {
        private IWindowService _workerService;
        public WindowsController(IWindowService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost("doWork")]
        public async Task<IActionResult> DoWork()
        {
            var result = await _workerService.DoWork();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
