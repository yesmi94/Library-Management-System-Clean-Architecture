using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.Commands
{
    public record BorrowBookCommand(string bookId, string personId) : IRequest<string>;
}
