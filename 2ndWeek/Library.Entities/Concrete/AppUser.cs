namespace Library.Entities.Concrete
{
    public class AppUser : AuditablePersonBaseEntity
    {
        public AppUser() 
        {
            AppUserBooks = new HashSet<AppUserBook>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string VerificationCode { get; set; }

        // Relations
        public virtual ICollection<AppUserBook> AppUserBooks { get; set; }
    }
}