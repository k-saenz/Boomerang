using System;
using System.ComponentModel.DataAnnotations;

namespace Boomerang.Models
{
    public class File
    {
        [Required]
        public int FileId { get; set; }
        //Stores User ID
        public int BelongsTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] FileByteArr { get; set; }
        public string FileType { get; set; }

        public File() { }

        public File(int fileId, int belongsTo, DateTime? createdOn, byte[] fileByteArr, string fileType)
        {
            FileId = fileId;
            BelongsTo = belongsTo;
            CreatedOn = createdOn;
            FileByteArr = fileByteArr;
            FileType = fileType;
        }

    }
}
