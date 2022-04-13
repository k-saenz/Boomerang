using System;
using System.ComponentModel.DataAnnotations;

namespace Boomerang.Models
{
    public class BoomerangFile
    {
        [Key]
        [Required]
        public int FileId { get; set; }
        //Stores User ID
        public string BelongsTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] Content { get; set; }
        public string FileType { get; set; }

        public BoomerangFile() { }

        public BoomerangFile(int fileId, string belongsTo, DateTime? createdOn, byte[] content, string fileType)
        {
            FileId = fileId;
            BelongsTo = belongsTo;
            CreatedOn = createdOn;
            Content = content;
            FileType = fileType;
        }

    }

}
