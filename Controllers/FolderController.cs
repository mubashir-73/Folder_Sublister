// the entire logic here is in controller sir, let me know if you want it in MVC format. 
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection.Metadata.Ecma335;

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
            // For now the files will be skipped if access is denied
            var results = new List<object>();
            try
            {
                foreach (var filePath in Directory.EnumerateFiles(
                               FolderPath, "*", SearchOption.AllDirectories))
                {
                    
                    try
                    {
                        var fileInfo = new FileInfo(filePath);

                        results.Add(new
                        {
                            FileName = fileInfo.Name,
                            Extension = fileInfo.Extension,
                            FilePath = filePath,
                            SizeKB = $"{Math.Round(fileInfo.Length / 1024.0, 2)} kb"
                        });
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // this will just do nothing hence hopefully it should skip the file.
                        continue;
                    }
                    catch (PathTooLongException)
                    {
                        continue;
                    }
                }
            }
            catch (UnauthorizedAccessException) 
            {
               return StatusCode(403,"Access has been denied to one or more Directories.");
            }
            return Ok(results);
        }
    }
}
