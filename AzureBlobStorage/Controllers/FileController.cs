using AzureBlobStorage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureBlobStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IStorageManager storageManager;

        public FileController(IStorageManager storageManager)
        {
            this.storageManager = storageManager;
        }

        [HttpGet("GetFile")]
        public IActionResult GetUrl(string pathName)
        {
            try
            {
                var result = storageManager.GetSignedUrl(pathName);
                return Ok(result);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpDelete("DeleteFile")]
        public IActionResult DeleteUrl(string pathName)
        {
            try
            {
                var result = storageManager.DeleteFile(pathName);
                return Ok(result);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpPost("PostFile")]
        public IActionResult PostFile(IFormFile formFile)
        {
            try
            {
                storageManager.UploadFile(formFile.OpenReadStream(), formFile.Name,formFile.ContentType);
                return Ok();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }
    }
}
