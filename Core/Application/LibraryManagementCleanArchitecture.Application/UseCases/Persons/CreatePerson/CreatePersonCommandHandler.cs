namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<string>>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreatePersonCommandHandler(IRepository<Person> personRepository, IUnitOfWork unitOfWork)
        {
            this.personRepository = personRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            Result<Person> personResult = request.Role switch
            {
                UserType.Member => Result<Person>.Success(new Member(request.Name)),
                UserType.MinorStaff => Result<Person>.Success(new MinorStaff(request.Name)),
                UserType.ManagementStaff => Result<Person>.Success(new ManagementStaff(request.Name)),
                _ => Result<Person>.Failure("Invalid type of member")
            };

            if (!personResult.IsSuccess) {
                return Result<string>.Failure(personResult.Error!);
            }

            Person person = personResult.Value!;

            await personRepository.AddAsync(person);
            await unitOfWork.CompleteAsync();
            return Result<string>.Success(person.Id);
        }
    }
}
