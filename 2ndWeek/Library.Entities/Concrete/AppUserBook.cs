namespace Library.Entities.Concrete
{
    public class AppUserBook : AuditableBaseEntity
    {
        // Relations
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
