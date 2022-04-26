using Boomerang.Data;
using Boomerang.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boomerang.Controllers
{
    public class PremiumController : Controller
    {
        private readonly BoomerangDbContext _dbcontext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PremiumController(BoomerangDbContext context, UserManager<User> uManager, RoleManager<IdentityRole> rManager)
        {
            _dbcontext = context;
            _userManager = uManager;
            _roleManager = rManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upgrade()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upgrade(string userId)
        {
            const string Premium = "Premium";

            var user = await _userManager.FindByNameAsync(userId);
            var premiumRole = _roleManager.FindByNameAsync(Premium).Result;
            var isInRole = _userManager.IsInRoleAsync(user, Premium).Result;

            if (isInRole)
            {
                return RedirectToPage("/Index");
            }

            await _userManager.AddToRoleAsync(user, premiumRole.Name);
            ViewData["success"] = "Account upgraded successfully";

            return View();
        }
    }
}
