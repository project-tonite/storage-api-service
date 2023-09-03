using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_api_service.Models.Repositories.IRepository
{
    public interface IFileRepository:IRepository<FileEntity>, IRepositoryAsync<FileEntity>
    {
        void Update(FileEntity file);
    }
}
