namespace Library.DataAccess.Context
{
    public class LibraryDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public const string ConnectionName = "conn";
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}