using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.DTO.BookDTO
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public BookCategory Category { get; set; }
    }
}
