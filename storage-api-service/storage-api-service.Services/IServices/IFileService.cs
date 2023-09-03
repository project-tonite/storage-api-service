using Microsoft.AspNetCore.Http;
using storage_api_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_api_service.Services.Interfaces
{
    public interface IFileService
    {
        Task<FileEntity> CreateFile(FileEntity file);
        Task<FileEntity> GetFileById(int fileId);
        Task<IEnumerable<FileEntity>> ListFiles();
        Task UpdateFile(FileEntity file);
        Task DeleteFile(int fileId);
        Task<byte[]> ReadFileData(IFormFile file);
    }
}
