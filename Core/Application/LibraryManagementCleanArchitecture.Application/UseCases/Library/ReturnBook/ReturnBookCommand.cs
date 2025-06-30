using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook
{
    public record ReturnBookCommand(string bookId, string personId) : IRequest<string>;
}
