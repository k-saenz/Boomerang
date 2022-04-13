using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Boomerang.Data;
using Boomerang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Boomerang.Views.Home
{
    public class UploadFileModel : PageModel
    {
        private readonly BoomerangDbContext _dbContext;

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            var memoryStream = new MemoryStream();
            await FileUpload.FormFile.CopyToAsync(memoryStream);

            // Upload the file if it is less than 2MB
            if (memoryStream.Length < 2097152)
            {
                var file = new BoomerangFile()
                {
                    BelongsTo = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    Content = memoryStream.ToArray(),
                    FileType = FileUpload.FormFile.ContentType
                };

                _dbContext.Files.Add(file);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("File", "The file is too large");
            }

            return Page();
        }
    }

    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name="File")]
        public IFormFile FormFile { get; set; }
    }
}


