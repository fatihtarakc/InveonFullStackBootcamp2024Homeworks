namespace Library.Entities.Configurations.Concrete
{
    public class AppUserBookConfiguration : AuditableBaseEntityConfiguration<AppUserBook>
    {
        public override void Configure(EntityTypeBuilder<AppUserBook> builder)
        {
            base.Configure(builder);

            builder.HasOne(appUserBook => appUserBook.AppUser).WithMany(appUser => appUser.AppUserBooks).HasForeignKey(appUserBook => appUserBook.AppUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(appUserBook => appUserBook.Book).WithMany(book => book.AppUserBooks).HasForeignKey(appUserBook => appUserBook.BookId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}