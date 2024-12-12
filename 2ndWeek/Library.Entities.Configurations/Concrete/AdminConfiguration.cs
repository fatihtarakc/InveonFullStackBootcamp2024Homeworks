namespace Library.Entities.Configurations.Concrete
{
    public class AdminConfiguration : AuditablePersonBaseEntityConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);
        }
    }
}