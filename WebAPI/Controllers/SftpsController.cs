using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Sftp.Dtos.SftpDownloadFileDtos;
using Sftp.Dtos.SftpUploadFileDtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SftpsController : ControllerBase
    {
        private ISftpService _sftpService;
        public SftpsController(ISftpService SftpService)
        {
            _sftpService = SftpService;
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(SftpUploadFileRequestDto SftpUploadFileRequestDto)
        {
            var result = await _sftpService.UploadFile(SftpUploadFileRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("sftpDownload")]
        public async Task<IActionResult> SftpDownload(SftpDownloadFileRequestDto SftpDownloadFileRequestDto)
        {
            var result = await _sftpService.SftpDownload(SftpDownloadFileRequestDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteFile")]
        public async Task<IActionResult> DeleteFile( string filePath)
        {
            var result = await _sftpService.DeleteFile( filePath);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getDirectory")]
        public async Task<IActionResult> GetDirectory(string prefix)
        {
            var dataResult = await _sftpService.GetDirectory(prefix);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

        [HttpGet("getFileNames")]
        public async Task<IActionResult> GetFileNames(string directory)
        {
            var dataResult = await _sftpService.GetFileNames(directory);
            if (dataResult.Success)
            {
                return Ok(dataResult);
            }
            return BadRequest(dataResult);
        }

    
    }
}
