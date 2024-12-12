namespace Library.Core.Entities.Abstract
{
    public abstract class AuditableBaseEntity : BaseEntity, ICreatableEntity, IDeletableEntity, IUpdatableEntity
    {
        public string CreatedBy { get; init; }
        public DateTime CreatedDate { get; init; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}