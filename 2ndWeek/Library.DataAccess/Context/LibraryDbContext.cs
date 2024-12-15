namespace Library.DataAccess.Context
{
    public class LibraryDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor? httpContextAccessor;
        //public LibraryDbContext() { }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, IHttpContextAccessor httpContextAccessor = null) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<Admin>? Admins { get; set; }
        public virtual DbSet<AppUser>? AppUsers { get; set; }
        public virtual DbSet<AppUserBook>? AppUserBooks { get; set; }
        public virtual DbSet<Book>? Books { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=localhost, 1434; Database=LibraryDb; User=sa; Password=Dockermssqldb2024+-!?; TrustServerCertificate=True;");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetBaseProperties()
        {
            var entityEntries = ChangeTracker.Entries<AuditableBaseEntity>();
            var userId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "User do not found !";

            foreach (var entityEntry in entityEntries)
            {
                SetIfAdded(entityEntry, userId);
                SetIfModified(entityEntry, userId);
                SetIfDeleted(entityEntry, userId);
            }
        }

        private void SetIfAdded(EntityEntry<AuditableBaseEntity> entityEntry, string userId)
        {
            if (entityEntry.State is not EntityState.Added) return;

            entityEntry.Entity.CreatedBy = userId;
            entityEntry.Entity.CreatedDate = DateTime.Now;
            entityEntry.Entity.Status = Status.Added;
        }

        private void SetIfDeleted(EntityEntry<AuditableBaseEntity> entityEntry, string userId)
        {
            if (entityEntry.State is not EntityState.Deleted) return;

            entityEntry.State = EntityState.Modified;
            entityEntry.Entity.DeletedBy = userId;
            entityEntry.Entity.DeletedDate = DateTime.Now;
            entityEntry.Entity.Status = Status.Deleted;
        }

        private void SetIfModified(EntityEntry<AuditableBaseEntity> entityEntry, string userId)
        {
            if (entityEntry.State is EntityState.Added) return;
            if (entityEntry.State is EntityState.Deleted) return;

            entityEntry.Entity.ModifiedBy = userId;
            entityEntry.Entity.ModifiedDate = DateTime.Now;
            entityEntry.Entity.Status = Status.Modified;
        }
    }
}