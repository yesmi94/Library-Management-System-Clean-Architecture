using MediatR;
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.Commands
{
    public record CreateBookCommand
    (
        string Title,
        string Author,
        string Year,
        BookCategory BookCategory

    ) : IRequest<string>;
}
