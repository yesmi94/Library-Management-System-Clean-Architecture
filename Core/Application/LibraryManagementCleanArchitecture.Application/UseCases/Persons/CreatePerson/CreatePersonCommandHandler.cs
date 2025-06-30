using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, string>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreatePersonCommandHandler(IRepository<Person> personRepository, IUnitOfWork unitOfWork)
        {
            this.personRepository = personRepository;
            this.unitOfWork = unitOfWork;
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
            await unitOfWork.CompleteAsync();
            return person.Id;
        }
    }
}
