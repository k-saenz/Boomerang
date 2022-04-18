using Boomerang.Data;
using Boomerang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            List<BoomerangFile> files = _dbcontext.Files
                .Where(f => f.BelongsTo == userId)
                .ToList();

            return View(files);
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(BoomerangFile model)
        {
            if (ModelState.IsValid)
            {

                if (model != null && model.Length > 0)
                {
                    var file = model;

                    file.CreatedOn = DateTime.Now;
                    file.BelongsTo = User.Identity.Name;

                    byte[] content;

                    var readStream = file.OpenReadStream();
                    var memoryStream = new MemoryStream();

                    await readStream.CopyToAsync(memoryStream);
                    content = memoryStream.ToArray();

                    file.Content = content;

                    _dbcontext.Files.Add(file);
                    await _dbcontext.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("File", "Upload a valid file");
                }

                return RedirectToPage("./UploadSuccessful");
            }
            else
            {
                return View(ViewData["error"] = "something went wrong");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
