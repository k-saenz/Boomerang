using Boomerang.Data;
using Boomerang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boomerang.Controllers
{
    [Authorize(Roles = "Premium")]
    public class PremiumController : Controller
    {
        private readonly BoomerangDbContext _dbcontext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public PremiumController(BoomerangDbContext context, UserManager<User> uManager, RoleManager<IdentityRole> rManager, SignInManager<User> sManager)
        {
            _dbcontext = context;
            _userManager = uManager;
            _roleManager = rManager;
            _signInManager = sManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Upgrade()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Upgrade(string userId)
        {
            const string Premium = "Premium";

            var user = await _userManager.FindByNameAsync(userId);
            var premiumRole = _roleManager.FindByNameAsync(Premium).Result;
            var isInRole = _userManager.IsInRoleAsync(user, Premium).Result;

            if (isInRole)
            {
                return RedirectToAction("Index", "Home");
            }

            await _userManager.RemoveFromRoleAsync(user, "Basic");
            await _userManager.AddToRoleAsync(user, premiumRole.Name);
            await ReloginUser(user);
            

            return RedirectToAction("Index", "Premium");
        }
        [HttpGet]
        public IActionResult Downgrade()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Downgrade(string userId)
        {
            const string Basic = "Basic";
            const string Premium = "Premium";

            var user = await _userManager.FindByNameAsync(userId);
            var basicRole = _roleManager.FindByNameAsync(Basic).Result;

            await _userManager.RemoveFromRoleAsync(user, Premium);
            await _userManager.AddToRoleAsync(user, basicRole.Name);
            await ReloginUser(user);

            return RedirectToAction("DowngradeSuccessful");
        }

        [AllowAnonymous]
        public IActionResult DowngradeSuccessful()
        {
            return View();
        }

        private async Task ReloginUser(User user)
        {
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);
        }
    }
}
