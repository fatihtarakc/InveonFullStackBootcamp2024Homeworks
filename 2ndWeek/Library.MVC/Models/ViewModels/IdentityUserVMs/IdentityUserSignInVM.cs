namespace Library.MVC.Models.ViewModels.IdentityUserVMs
{
    public class IdentityUserSignInVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsPersistant { get; set; }
    }
}