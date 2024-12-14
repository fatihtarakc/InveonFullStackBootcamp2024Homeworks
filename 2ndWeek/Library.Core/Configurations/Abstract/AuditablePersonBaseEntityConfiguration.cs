namespace Library.Core.Configurations.Abstract
{
    public abstract class AuditablePersonBaseEntityConfiguration<T> : AuditableBaseEntityConfiguration<T> where T : AuditablePersonBaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(auditablePersonBaseEntity => auditablePersonBaseEntity.Email).HasColumnType("varchar").HasMaxLength(50);

            builder.ToTable(auditablePersonBaseEntity => auditablePersonBaseEntity.HasCheckConstraint("Email_MinLength_Control", "Len(Email) >= 5"));
            
            builder.Property(auditablePersonBaseEntity => auditablePersonBaseEntity.IdentityId).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}