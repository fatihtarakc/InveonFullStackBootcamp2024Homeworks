namespace Library.MVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(IdentityUserSignInVM identityUserSignInVM)
        {
            if (!ModelState.IsValid) return View(identityUserSignInVM);

            var identityUser = await accountService.FindByEmailAsync(identityUserSignInVM.Email);
            if (identityUser is null)
            {
                NotifyError(Message.Account_Login_Failed);
                return View(identityUserSignInVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await accountService.PasswordSignInAsync(identityUser, identityUserSignInVM.Password, identityUserSignInVM.IsPersistant);
            if (!signInResult.Succeeded) 
            {
                NotifyError(Message.Account_Login_Failed);
                return View(identityUserSignInVM);
            }

            var role = await accountService.GetRoleAsync(identityUser);
            if (role is null)
            {
                NotifyError(Message.Account_Login_Failed);
                return View(identityUserSignInVM);
            }
            if (role is Roles.Admin) return RedirectToAction("Index", "Home", new { area = "Admin" });
            if (role is Roles.AppUser) return RedirectToAction("Index", "Home", new { area = "AppUser" });

            return RedirectToAction("Index", "NotFound");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(string identityUserSignUpVM)
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await accountService.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}