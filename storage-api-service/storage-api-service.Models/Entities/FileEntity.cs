using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_api_service.Models
{
    public class FileEntity
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
