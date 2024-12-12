namespace Library.Entities.Configurations.Concrete
{
    public class UserConfiguration : AuditablePersonBaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
        }
    }
}