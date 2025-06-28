
using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Persistance.Configurations
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.ToTable("People");

            builder.HasDiscriminator<UserType>("Role")
                .HasValue<Member>(UserType.Member)
                .HasValue<MinorStaff>(UserType.MinorStaff)
                .HasValue<ManagementStaff>(UserType.ManagementStaff);


            builder.HasKey(person => person.Id);

            builder.Property(person => person.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(person => person.Role)
                .IsRequired();

        }
    }
}
