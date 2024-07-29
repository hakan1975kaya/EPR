using Business.Abstract;
using Entities.Concrete.Dtos.MenuDtos.MenuAddDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuUpdateDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSaveDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private IMenuService _menuService;
        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataResult = await _menuService.GetById(id);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var dataResult = await _menuService.GetList();
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(MenuAddRequestDto menuAddRequestDto)
        {
            var result = await _menuService.Add(menuAddRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(MenuUpdateRequestDto menuUpdateRequestDto)
        {
            var result = await _menuService.Update(menuUpdateRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _menuService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("menuParentListGetByUserId")]
        public async Task<IActionResult> MenuParentListGetByUserId(int userId)
        {
            var dataResult = await _menuService.MenuParentListGetByUserId(userId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("menuChildListGetByUserId")]
        public async Task<IActionResult> MenuChildListGetByUserId(int userId)
        {
            var dataResult = await _menuService.MenuChildListGetByUserId(userId);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(MenuSearchRequestDto menuSearchRequestDto)
        {
            var dataResult = await _menuService.Search(menuSearchRequestDto);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(MenuSaveRequestDto menuSaveRequestDto)
        {
            var result = await _menuService.Save(menuSaveRequestDto);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
