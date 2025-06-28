using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, string>
    {
        private readonly IRepository<Person> personRepository;

        public CreatePersonCommandHandler(IRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<string> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            Person person = request.Role switch
            {
                UserType.Member => new Member(request.Name),
                UserType.MinorStaff => new MinorStaff(request.Name),
                UserType.ManagementStaff => new ManagementStaff(request.Name),
                _ => throw new InvalidPersonException("Invalid type of member")
            };

            await personRepository.AddAsync(person);
            return person.Id;
        }
    }
}
