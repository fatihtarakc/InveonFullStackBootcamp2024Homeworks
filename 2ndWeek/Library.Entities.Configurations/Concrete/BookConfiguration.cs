namespace Library.Entities.Configurations.Concrete
{
    public class BookConfiguration : AuditableBaseEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
        }
    }
}