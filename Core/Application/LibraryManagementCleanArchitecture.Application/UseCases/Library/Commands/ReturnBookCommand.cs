

using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.Commands
{
    public record ReturnBookCommand(string bookId, string personId) : IRequest<string>;
}
