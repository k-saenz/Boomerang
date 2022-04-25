using Boomerang.Data;
using Boomerang.Models;
using Boomerang.Models.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boomerang.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoomerangDbContext _dbcontext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BoomerangDbContext context, ILogger<HomeController> logger)
        {
            _dbcontext = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string userId = User.Identity.Name;
            var files = _dbcontext.Files
                .Where(f => f.BelongsTo == userId)
                .ToList();

            var data = _dbcontext.Files
                .Join(
                    _dbcontext.FileData,
                    f => f.FileId,
                    fd => fd.BoomerangFileId,
                    (file, filedata) => new FileDataFromJoin
                    {
                        FileId = file.FileId,
                        CreatedOn = file.CreatedOn,
                        Content = file.Content,
                        ContentType = filedata.ContentType,
                        FileName = filedata.FileName,
                    }
                ).ToList();

            return View(data);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UploadFile()
        {
            return View(new BoomerangFile());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile newFile)
        {

            if (ModelState.IsValid)
            {

                if (newFile != null && newFile.Length > 0)
                {
                    var data = new FileData(newFile);

                    var file = new BoomerangFile
                    {
                        BelongsTo = User.Identity.Name,
                        FileData = data,
                        CreatedOn = DateTime.Now,
                    };

                    byte[] content = GetByteArrayFromFile(newFile);

                    file.Content = content;

                    _dbcontext.Files.Add(file);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction("Index", ViewData["uploadMessage"] = "Upload Successful!");
                }
                else
                {
                    ModelState.AddModelError("File", "Upload a valid file");
                }
            }
            return View(ViewData["error"] = "something went wrong");
        }

        [HttpPost]
        [Authorize]
        public FileContentResult Download(int id)
        {
            var file = _dbcontext.Files
                .Where(f => f.FileId == id)
                .First();
            var filedata = _dbcontext.FileData
                .Where(d => d.BoomerangFileId == id)
                .Select(d => d.ContentType)
                .First();

            return File(file.Content, filedata, file.Name);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var fileToDelete = _dbcontext.Files
                .Where(f => f.FileId == id)
                .First();
            _dbcontext.Files.Remove(fileToDelete);
            await _dbcontext.SaveChangesAsync();
            _logger.LogInformation(id + "was deleted");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Util methods
        private byte[] GetByteArrayFromFile(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public class FileDataFromJoin
        {
            public int FileId { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string ContentType { get; set; }
            public byte[] Content { get; set; }
            public string FileName { get; set; }
        }

    }
}
