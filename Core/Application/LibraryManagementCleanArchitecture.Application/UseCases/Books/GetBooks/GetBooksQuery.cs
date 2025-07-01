using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.GetBooks
{
    public record GetBooksQuery(string personId) : IRequest<Result<List<BookDto>>>;

}
