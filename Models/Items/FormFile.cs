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
    public class FormFile : IFormFile
    {
        private readonly IFormFile _file;

        //Properties
        [Key]
        [Required]
        public string FileDataId { get; set; }
        public BoomerangFile File { get; set; }

        public string ContentType
        {
            get { return _file.ContentType; }

        }

        [NotMapped]
        public string ContentDisposition
        {
            get { return _file.ContentDisposition; }

        }
        [NotMapped]
        public IHeaderDictionary Headers
        {
            get { return _file.Headers; }

        }

        [Column(TypeName = "BIGINT")]
        public long Length
        {
            get { return _file.Length; }

        }

        public string Name
        {
            get { return _file.Name; }

        }

        public string FileName
        {
            get { return _file.FileName; }

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
