namespace Library.Core.Configurations.Abstract
{
    public abstract class AuditablePersonBaseEntityConfiguration<T> : AuditableBaseEntityConfiguration<T> where T : AuditablePersonBaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(auditablePersonBaseEntity => auditablePersonBaseEntity.Name).HasMaxLength(25);
            builder.Property(auditablePersonBaseEntity => auditablePersonBaseEntity.Surname).HasMaxLength(25);
            builder.Property(auditablePersonBaseEntity => auditablePersonBaseEntity.Email).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}