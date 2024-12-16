namespace Library.Business.Concrete.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression) =>
            await userManager.Users.AnyAsync(expression);

        public async Task<IdentityResult> AddAsync(IdentityUser identityUser, Roles role)
        {
            var identityResult = await userManager.CreateAsync(identityUser);
            if (!identityResult.Succeeded) return identityResult;

            return await userManager.AddToRoleAsync(identityUser, role.ToString());
        }

        public async Task<IdentityUser> FindByEmailAsync(string email) =>
            await userManager.FindByEmailAsync(email);

        public async Task<IdentityUser> FindByIdAsync(string id) =>
            await userManager.FindByIdAsync(id);

        public async Task<IdentityUser> FindByNameAsync(string username) =>
             await userManager.FindByNameAsync(username);

        public async Task<Roles?> GetRoleAsync(IdentityUser identityUser) =>
            Enum.Parse<Roles>((await userManager.GetRolesAsync(identityUser)).FirstOrDefault());

        public async Task<SignInResult> PasswordSignInAsync(IdentityUser identityUser, string password, bool isPersistent = false, bool isLockoutOnFailure = false) =>
            await signInManager.PasswordSignInAsync(identityUser, password, isPersistent, isLockoutOnFailure);

        public async Task SignOutAsync() =>
            await signInManager.SignOutAsync();
    }
}