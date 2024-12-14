namespace Library.Entities.Configurations.Concrete
{
    public class BookConfiguration : AuditableBaseEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);

            builder.Property(book => book.Title).HasColumnType("nvarchar");
            builder.ToTable(book => book.HasCheckConstraint("Title_MinLength_Control", "Len(Title) >= 1"));

            builder.ToTable(book => book.HasCheckConstraint("PageCount_Min_Control", "PageCount >= 1"));

            builder.Property(book => book.Author).HasColumnType("nvarchar").HasMaxLength(50);
            builder.ToTable(book => book.HasCheckConstraint("Author_MinLength_Control", "Len(Author) >= 2"));

            builder.Property(book => book.Genre).HasColumnType("nvarchar").HasMaxLength(50);
            builder.ToTable(book => book.HasCheckConstraint("Genre_MinLength_Control", "Len(Genre) >= 5"));

            builder.Property(book => book.Language).HasColumnType("nvarchar").HasMaxLength(20);
            builder.ToTable(book => book.HasCheckConstraint("Language_MinLength_Control", "Len(Language) >= 2"));

            builder.ToTable(book => book.HasCheckConstraint("PublicationYear_Min_Control", "PublicationYear >= 1"));
            builder.ToTable(book => book.HasCheckConstraint("PublicationYear_Max_Control", "PublicationYear <= Year(GetDate())"));

            builder.Property(book => book.Publisher).HasColumnType("nvarchar").HasMaxLength(50);
            builder.ToTable(book => book.HasCheckConstraint("Publisher_MinLength_Control", "Len(Publisher) >= 1"));

            builder.Property(book => book.ISBN).HasColumnType("varchar").HasDefaultValue(HelperISBN.Generator());

            builder.Property(book => book.Summary).HasColumnType("nvarchar");
            builder.ToTable(book => book.HasCheckConstraint("Summary_MinLength_Control", "Len(Summary) >= 5"));

            builder.ToTable(book => book.HasCheckConstraint("AvailableCopies_Min_Control", "AvailableCopies >= 1"));
        }
    }
}