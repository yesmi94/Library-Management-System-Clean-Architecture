
using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.Queries
{
    public record GetBooksQuery(string personId) : IRequest<List<BookDto>>;

}
