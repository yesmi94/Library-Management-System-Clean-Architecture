namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{

    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public record CreatePersonCommand(
        string Name,
        UserType Role,
        int BorrowedBooksNum
        ) : IRequest<Result<string>>;
}
