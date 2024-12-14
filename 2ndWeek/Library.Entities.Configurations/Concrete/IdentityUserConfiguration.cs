namespace Library.Entities.Configurations.Concrete
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasIndex(identityUser => identityUser.Email).IsUnique();
            builder.Property(identityUser => identityUser.Email).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.ToTable(identityUser => identityUser.HasCheckConstraint("Email_MinLength_Control", "Len(Email) >= 5"));

            builder.HasIndex(identityUser => identityUser.UserName).IsUnique();
            builder.Property(identityUser => identityUser.UserName).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.ToTable(identityUser => identityUser.HasCheckConstraint("UserName_MinLength_Control", "Len(UserName) >= 5"));

            builder.HasIndex(identityUser => identityUser.NormalizedEmail).IsUnique();
            builder.Property(identityUser => identityUser.NormalizedEmail).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.ToTable(identityUser => identityUser.HasCheckConstraint("NormalizedEmail_MinLength_Control", "Len(NormalizedEmail) >= 5"));

            builder.HasIndex(identityUser => identityUser.NormalizedUserName).IsUnique();
            builder.Property(identityUser => identityUser.NormalizedUserName).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.ToTable(identityUser => identityUser.HasCheckConstraint("NormalizedUserName_MinLength_Control", "Len(NormalizedUserName) >= 5"));
        }
    }
}