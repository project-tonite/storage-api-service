using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using storage_api_service.Models;
using storage_api_service.Services;

namespace storage_api_service.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly FileService _fileService;

        public FilesController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile(FileEntity file)
        {
            var createdFile = await _fileService.CreateFile(file);
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
        public async Task<IActionResult> UpdateFile(int id, FileEntity file)
        {
            if (id != file.Id)
            {
                return BadRequest();
            }
            await _fileService.UpdateFile(file);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await _fileService.DeleteFile(id);
            return NoContent();
        }
    }

}
