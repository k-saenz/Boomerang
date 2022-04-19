using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Boomerang.Models
{
    public class BoomerangFile : IFormFile
    {
        [Key]
        [Required]
        public int FileId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        //Stores User ID
        public string BelongsTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public long Length { get; }

        public string ContentDisposition => throw new NotImplementedException();

        public IHeaderDictionary Headers => throw new NotImplementedException();

        public BoomerangFile() { }

        public BoomerangFile(int fileId, string belongsTo, DateTime? createdOn, byte[] content, string fileType)
        {
            FileId = fileId;
            BelongsTo = belongsTo;
            CreatedOn = createdOn;
            Content = content;
            ContentType = fileType;
        }

        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}
