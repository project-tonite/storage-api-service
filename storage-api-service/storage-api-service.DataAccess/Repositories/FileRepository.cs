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
            var objFromDb = _db.Files.FirstOrDefault(x => x.Id == obj.Id);

            if(objFromDb is not null)
            {
                objFromDb.UpdatedAt = DateTime.UtcNow;
                objFromDb.Content = obj.Content;
                objFromDb.Version = obj.Version;
                objFromDb.FileName = obj.FileName;
                _db.Update(objFromDb);
            }
          
            //To DO: Throw not found exception and handle it from Global exceptions
         
        }
        public async Task SaveChangesAsync()
        {
           await  _db.SaveChangesAsync();
        }
    }
}
