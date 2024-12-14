namespace Library.Entities.Concrete
{
    public class AppUserBook : AuditableBaseEntity
    {
        // Relations
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
