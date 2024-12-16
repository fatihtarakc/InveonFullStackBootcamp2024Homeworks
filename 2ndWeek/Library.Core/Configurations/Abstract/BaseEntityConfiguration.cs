namespace Library.Core.Configurations.Abstract
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(baseEntity => baseEntity.Id);
            builder.Property(baseEntity => baseEntity.Id).ValueGeneratedOnAdd();
            builder.Property(baseEntity => baseEntity.Status).HasConversion(role => role.ToString(), role => (Status)Enum.Parse<Roles>(role));
        }
    }
}