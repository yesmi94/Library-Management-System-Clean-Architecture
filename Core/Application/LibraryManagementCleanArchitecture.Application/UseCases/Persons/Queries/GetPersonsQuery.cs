using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.Queries
{
    public record GetPersonsQuery() : IRequest<List<PersonDto>>;

}
