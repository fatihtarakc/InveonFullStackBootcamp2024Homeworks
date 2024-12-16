namespace Library.DataAccess.Concrete.SeedDatas
{
    internal static class AdminSeedData
    {
        private const string email = "admin@library.com";
        private const string username = "admin";
        private const string password = "Admin2024+-!?";
        internal static async Task AddAsync(IConfiguration configuration, IOptions<ConnectionOptions> iOptionsConnectionOptions, UserManager<IdentityUser> userManager)
        {
            var connectionOptions = iOptionsConnectionOptions.Value;
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionOptions?.MssqlServerConnectionString);

            using LibraryDbContext db = new(dbContextOptionsBuilder.Options);
            if (!(await db.Roles.AnyAsync())) await AddRolesAsync(db);

            if ((await userManager.FindByEmailAsync(email) is null ? false : true) && (await userManager.FindByEmailAsync(email) is null ? false : true) is false) await AddAdminAsync(db, userManager);

            await Task.CompletedTask;
        }

        private static async Task AddAdminAsync(LibraryDbContext db, UserManager<IdentityUser> userManager)
        {
            IdentityUser identityUser = new()
            {
                EmailConfirmed = true,
                Email = email.ToLowerInvariant(),
                UserName = username.ToLowerInvariant(),
                NormalizedEmail = email.ToUpperInvariant(),
                NormalizedUserName = username.ToUpperInvariant()
            };
            identityUser.PasswordHash = userManager.PasswordHasher.HashPassword(identityUser, password);
            await userManager.CreateAsync(identityUser);
            await userManager.AddToRoleAsync(identityUser, Roles.Admin.ToString());

            Admin admin = new()
            {
                Email = email.ToLowerInvariant(),
                IdentityId = identityUser.Id,
                Status = Status.Added,
                CreatedBy = "super admin",
                CreatedDate = DateTime.Now
            };
            await db.Admins.AddAsync(admin);
            await db.SaveChangesAsync();
        }

        private static async Task AddRolesAsync(LibraryDbContext db)
        {
            List<Roles> roles = Enum.GetValues<Roles>().Cast<Roles>().ToList();
            foreach (var role in roles)
            {
                if (await db.Roles.AnyAsync(dbRole => dbRole.Name == role.ToString())) continue;

                await db.Roles.AddAsync(new IdentityRole { Name = role.ToString(), NormalizedName = role.ToString().ToUpperInvariant() });
                await db.SaveChangesAsync();
            }
        }
    }
}