using Business.Abstract;
using Entities.Concrete.Dtos.HcpDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HcpsController : ControllerBase
    {
        private readonly IHcpService _hcpService;
        public HcpsController(IHcpService hcpService)
        {
            _hcpService = hcpService;
        }

        [HttpPost("getFile")]
        public async Task<IActionResult> GetFile([FromQuery] HcpRequestDto hcpRequestDto)
        {
            var file = await _hcpService.GetFileByName(hcpRequestDto.FileName);
            var mimeType = GetMimeType(hcpRequestDto.FileName);
            return File(file.@return.byteFile, mimeType);
        }

        [HttpPost("downloadFile")]
        public async Task<IActionResult> DownloadFile(HcpRequestDto hcpRequestDto)
        {
            var file = await _hcpService.GetFileByName(hcpRequestDto.FileName);
            return File(file.@return.byteFile, "application/octet-stream", hcpRequestDto.FileName);
        }

        [HttpPost("uploadFile"), RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] HcpUploadRequestDto hcpUploadRequestDto)
        {
            await using var memoryStream = new MemoryStream();
            await hcpUploadRequestDto.File.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            await _hcpService.AddFile(hcpUploadRequestDto.File.FileName, bytes);
            return Ok("Dosya Kaydedildi");
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "image/jpeg";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
