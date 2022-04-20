using Boomerang.Models.Items;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Boomerang.Models
{
    public class BoomerangFile
    {
        [Key]
        [Required]
        public int FileId { get; set; }
        public string Name { get; set; }
        //Stores User ID
        public string BelongsTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] Content { get; set; }

        public FileData FileData { get; set; }

        public BoomerangFile() { }

        public BoomerangFile(int fileId, string belongsTo, DateTime? createdOn, byte[] content, FileData data)
        {
            FileId = fileId;
            BelongsTo = belongsTo;
            CreatedOn = createdOn;
            Content = content;
            FileData = data;
        }
    }

}
