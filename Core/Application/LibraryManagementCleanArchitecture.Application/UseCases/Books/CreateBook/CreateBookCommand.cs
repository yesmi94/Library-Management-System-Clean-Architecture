using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook
{
    public record CreateBookCommand
    (
        string Title,
        string Author,
        string Year,
        BookCategory BookCategory

    ) : IRequest<string>;
}
