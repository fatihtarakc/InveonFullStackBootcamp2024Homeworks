namespace Library.Core.Configurations.Abstract
{
    public abstract class AuditableBaseEntityConfiguration<T> : BaseEntityConfiguration<T> where T : AuditableBaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(auditableEntity => auditableEntity.CreatedBy).HasMaxLength(50);
            builder.Property(auditableEntity => auditableEntity.DeletedBy).HasMaxLength(50);
            builder.Property(auditableEntity => auditableEntity.ModifiedBy).HasMaxLength(50);
        }
    }
}