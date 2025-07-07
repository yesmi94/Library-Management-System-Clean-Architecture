namespace LibraryManagementCleanArchitecture.Persistance.Configurations
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LoginConfiguration : IEntityTypeConfiguration<LoginInfo>
    {
        public void Configure(EntityTypeBuilder<LoginInfo> builder)
        {
            builder.HasKey(login => login.LoginId);
        }
    }
}
