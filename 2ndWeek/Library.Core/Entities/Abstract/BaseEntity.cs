namespace Library.Core.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}