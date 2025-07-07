// <copyright file="BookConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Persistance.Configurations
{
	using LibraryManagementCleanArchitecture.Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(book => book.Id);

			builder.Property(book => book.Title)
				.IsRequired()
				.HasColumnType("nvarchar(450)");

			builder.Property(builder => builder.Author)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(builder => builder.Year).IsRequired();

			builder.Property(builder => builder.Category).IsRequired();

			builder.Property(builder => builder.IsAvailable)
				.IsRequired();
		}
	}
}
