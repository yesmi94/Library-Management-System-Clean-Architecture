namespace LibraryManagementCleanArchitecture.Persistance.Configurations
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
    {
        public void Configure(EntityTypeBuilder<Borrowing> builder)
        {
            builder.HasKey(borrowing => borrowing.Id);

            builder
                .HasOne(borrowing => borrowing.Person)
                .WithMany()
                .HasForeignKey(borrowing => borrowing.MemberId);

            builder
                .HasOne(borrowing => borrowing.Book)
                .WithMany()
                .HasForeignKey(borrowing => borrowing.BookId);
        }
    }
}
