using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.DTO.BookDTO
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public BookCategory Category { get; set; }
        public bool IsAvailable { get; set; }

    }
}
