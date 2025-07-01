using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    public record CreatePersonCommand(
        string Name,
        UserType Role,
        int BorrowedBooksNum
        ) : IRequest<string>;
}
