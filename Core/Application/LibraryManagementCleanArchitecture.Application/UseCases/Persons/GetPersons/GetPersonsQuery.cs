using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.Queries
{
    public record GetPersonsQuery() : IRequest<List<PersonDto>>;

}
