namespace Library.Business.Services.Abstract
{
    public interface IAccountService
    {
        Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression);

        Task<IdentityResult> CreateAsync(IdentityUser identityUser, Roles role);

        Task<IdentityUser> FindByEmailAsync(string email);
        
        Task<IdentityUser> FindByIdAsync(string identityUserId);

        Task<Roles?> GetRoleAsync(IdentityUser identityUser);

        Task<SignInResult> PasswordSignInAsync(IdentityUser identityUser, string password, bool isPersistent = false, bool isLockoutOnFailure = false);

        Task SignOutAsync();
    }
}