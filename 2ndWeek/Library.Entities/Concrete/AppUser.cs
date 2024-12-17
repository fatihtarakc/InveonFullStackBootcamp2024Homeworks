namespace Library.Entities.Concrete
{
    public class AppUser : AuditablePersonBaseEntity
    {
        public AppUser() 
        {
            Books = new HashSet<Book>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string VerificationCode { get; set; }

        // Relations
        public virtual ICollection<Book> Books { get; set; }
    }
}