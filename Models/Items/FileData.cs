using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boomerang.Models.Items
{
    public class FileData : IFormFile
    {
        private readonly FormFile _file;

        //Properties
        [Key]
        [Required]
        public string FileDataId { get; set; }

        public int BoomerangFileId { get; set; }
        public BoomerangFile File { get; set; }

        public string ContentType
        {
            get { return _file.ContentType; }
            set { this.ContentType = value; }
        }

        [NotMapped]
        public string ContentDisposition
        {
            get { return _file.ContentDisposition; }
            set { this.ContentDisposition = value; }
        }

        [NotMapped]
        public IHeaderDictionary Headers
        {
            get { return _file.Headers; }
            set { this.Headers = value; }
        }

        [Column(TypeName = "BIGINT")]
        public long Length
        {
            get { return _file.Length; }
            set { this.Length = value; }
        }

        public string Name
        {
            get { return _file.Name; }
            set { this.Name = value; }
        }

        public string FileName
        {
            get { return _file.FileName; }
            set { this.FileName = value; }
        }

        //Constructors
        public FileData() { }

        public FileData(string fileDataId, BoomerangFile file, string contentType, string contentDisposition, IHeaderDictionary headers, long length, string name, string fileName)
        {
            FileDataId = fileDataId;
            File = file;
            ContentType = contentType;
            ContentDisposition = contentDisposition;
            Headers = headers;
            Length = length;
            Name = name;
            FileName = fileName;

        }

        //Methods
        public void CopyTo(Stream target)
        {
            _file.CopyTo(target);
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            return _file.CopyToAsync(target, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            return _file.OpenReadStream();
        }
    }
}
