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





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
