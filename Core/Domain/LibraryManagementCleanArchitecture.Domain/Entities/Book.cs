
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
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
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Category = category;
            IsAvailable = isAvailable;
        }

        public Book()
        { 
        }
    }
}
