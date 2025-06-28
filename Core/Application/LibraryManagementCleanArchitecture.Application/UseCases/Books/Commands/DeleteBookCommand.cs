using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.Commands
{
    public record DeleteBookCommand(string BookId) : IRequest<string>;
}
