using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.DeleteBook
{
    public record DeleteBookCommand(string BookId) : IRequest<Result<string>>;
}
