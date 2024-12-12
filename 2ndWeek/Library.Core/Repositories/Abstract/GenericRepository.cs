namespace Library.Core.Repositories.Abstract
{
    public abstract class GenericRepository<Entity> : 
        IAsyncAddableRepository<Entity>, IAsyncDeletableRepository<Entity>, IAsyncUpdatableRepository<Entity>,
        IAsyncQueryableRepository<Entity>, IAsyncOrderableRepository<Entity> where Entity : AuditableBaseEntity
    {
        protected readonly DbSet<Entity> dbEntity;
        private readonly IdentityDbContext<IdentityUser, IdentityRole, string> db;
        protected GenericRepository(IdentityDbContext<IdentityUser, IdentityRole, string> db)
        {
            this.db = db;
            dbEntity = db.Set<Entity>();
        }

        public async Task<Entity> AddAsync(Entity entity)  => (await dbEntity.AddAsync(entity)).Entity;

        public Task DeleteAsync(Entity entity) => Task.FromResult(dbEntity.Remove(entity));
    }
}