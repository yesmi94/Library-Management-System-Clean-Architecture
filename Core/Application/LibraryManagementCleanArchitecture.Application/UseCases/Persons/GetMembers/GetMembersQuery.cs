using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetMembers
{
    public record GetMembersQuery() : IRequest<Result<List<PersonDto>>>;
}
