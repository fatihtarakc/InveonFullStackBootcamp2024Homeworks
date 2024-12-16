namespace Library.BackgroundJobs
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var userRole = httpContext.User.FindFirstValue(ClaimTypes.Role);
            return userRole == Roles.Admin.ToString() || userRole == Roles.AppUser.ToString();
        }
    }
}