using storage_api_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace storage_api_service.Models.Repositories.IRepository
{
    public class FileRepository : Repository<FileEntity>, IFileRepository
    {
        private readonly FileDbContext _db;

        public FileRepository(FileDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FileEntity obj)
        {
            _db.Update(obj);
        }
        public async Task SaveChangesAsync()
        {
           await  _db.SaveChangesAsync();
        }
    }
}
