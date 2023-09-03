using Microsoft.AspNetCore.Http;
using storage_api_service.Models;
using storage_api_service.Models.Repositories.IRepository;
using storage_api_service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_api_service.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileEntity> CreateFile(FileEntity file)
        {
            _fileRepository.Add(file);
            await _fileRepository.SaveChangesAsync();
            return file;
        }

        public async Task<FileEntity> GetFileById(int fileId)
        {
            return await _fileRepository.GetAsync(fileId);
        }

        public async Task<IEnumerable<FileEntity>> ListFiles()
        {
            return await _fileRepository.GetAllAsync();
        }

        public async Task UpdateFile(FileEntity file)
        {
            _fileRepository.Update(file);
            await _fileRepository.SaveChangesAsync();
        }

        public async Task DeleteFile(int fileId)
        {
            await _fileRepository.RemoveAsync(fileId);
            await _fileRepository.SaveChangesAsync();
        }

        public async Task<byte[]> ReadFileData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }


}
