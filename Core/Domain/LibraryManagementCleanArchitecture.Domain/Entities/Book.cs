// <copyright file="Book.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string Title { get; set; }

        public string Author { get; set; }

        public string Year { get; set; }

        public BookCategory Category { get; set; }

        public bool IsAvailable { get; set; }

        public Book(string id, string title, string author, string year, BookCategory category, bool isAvailable)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.Year = year;
            this.Category = category;
            this.IsAvailable = isAvailable;
        }

        public Book()
        {
        }
    }
}
