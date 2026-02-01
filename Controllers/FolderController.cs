using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Folder_Sublister.Controllers
{
    [Route("/api/files")]
    public class FolderController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFiles([FromQuery] string FolderPath)
        {
            if (string.IsNullOrEmpty(FolderPath) || !Directory.Exists(FolderPath))
            {
                return BadRequest("Invalid folder path.");
            }
            // I have added a try catch block here because there was some permission issues for few directories. Can you let me know if there is any way of resolving this?
            try
            {
                var files = Directory.GetFiles(FolderPath, "*", SearchOption.AllDirectories);
                var result = files.Select(filePath => new
                {
                    FileName = Path.GetFileName(filePath),
                    FileSize = new FileInfo(filePath).Length,
                    Extension = Path.GetExtension(filePath),
                    FilePath = filePath
                });
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
    }
}
