namespace Library.Entities.Configurations.Concrete
{
    public class AppUserConfiguration : AuditablePersonBaseEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.Property(appUser => appUser.Name).HasMaxLength(25);
            builder.Property(appUser => appUser.Surname).HasMaxLength(25);
            builder.Property(appUser => appUser.VerificationCode).HasDefaultValue(HelperVerification.CodeGenerator());

            builder.ToTable(appUser => appUser.HasCheckConstraint("Name_MinLength_Control", "Len(Name) >= 2"));
            builder.ToTable(appUser => appUser.HasCheckConstraint("Surname_MinLength_Control", "Len(Surname) >= 2"));
        }
    }
}