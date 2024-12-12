namespace Library.Core.Entities.Abstract
{
    public abstract class AuditablePersonBaseEntity : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}