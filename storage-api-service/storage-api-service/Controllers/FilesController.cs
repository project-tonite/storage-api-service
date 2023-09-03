using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using storage_api_service.Models;
using storage_api_service.Services;
using storage_api_service.Services.Interfaces;

namespace storage_api_service.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile([FromForm] IFormFile file, [FromForm] string Version)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            // Process and save the file to the database
            var fileEntity = new FileEntity
            {
                FileName = file.FileName,
                Content = await _fileService.ReadFileData(file),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Version = Version
            };
            var createdFile = await _fileService.CreateFile(fileEntity);
            return CreatedAtAction(nameof(GetFile), new { id = createdFile.Id }, createdFile);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _fileService.GetFileById(id);
            if (file == null)
            {
                return NotFound();
            }
            return Ok(file);
        }

        [HttpGet]
        public async Task<IActionResult> ListFiles()
        {
            var files = await _fileService.ListFiles();
            return Ok(files);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFile(
        int id,
        [FromForm] IFormFile file,
        [FromForm] string Version = null)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

           
            var fileEntity = new FileEntity
            {
                Id = id, 
                FileName = file.FileName, // Use provided FileName if available, or the original FileName
                Content = await _fileService.ReadFileData(file),
                UpdatedAt = DateTime.UtcNow,
                Version = Version
            };

            try
            {
                await _fileService.UpdateFile(fileEntity);
                return NoContent(); // Successfully updated
            }
            catch (FileNotFoundException)
            {
                return NotFound("File not found"); // Handle if the file with the specified ID is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await _fileService.DeleteFile(id);
            return NoContent();
        }
    }

}
