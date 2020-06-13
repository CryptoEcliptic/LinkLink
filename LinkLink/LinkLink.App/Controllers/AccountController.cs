using LinkLink.App.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkLink.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Username
                };

                IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        public async Task<JsonResult> IsExistingEmail(string email)
        {
            IdentityUser user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            return Json($"The email: {email} already exists!");
        }

        public async Task<JsonResult> IsExistingUsername(string username)
        {
            IdentityUser user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json(true);
            }
            return Json($"User with username: {username} already exists!");
        }
    }
}
