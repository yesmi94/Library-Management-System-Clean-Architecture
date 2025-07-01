namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.GetBooks
{
    using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
    using MediatR;

    public record GetBooksQuery(string personId) : IRequest<Result<List<BookDto>>>;

}
