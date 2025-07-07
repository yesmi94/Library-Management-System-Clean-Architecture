// <copyright file="CreatePersonCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<string>>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<LoginInfo> loginInfoRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreatePersonCommandHandler(IRepository<Person> personRepository, IUnitOfWork unitOfWork, IRepository<LoginInfo> loginInfoRepository)
        {
            this.personRepository = personRepository;
            this.unitOfWork = unitOfWork;
            this.loginInfoRepository = loginInfoRepository;
        }

        public async Task<Result<string>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await loginInfoRepository.GetByUsernameAsync(request.username);

            if(existingUser != null)
            {
                return Result<string>.Failure($"User with username {request.username} already exists. Try using a different username.");
            }
            Result<Person> personResult = request.role switch
            {
                UserType.Member => Result<Person>.Success(new Member(request.name)),
                UserType.MinorStaff => Result<Person>.Success(new MinorStaff(request.name)),
                UserType.ManagementStaff => Result<Person>.Success(new ManagementStaff(request.name)),
                _ => Result<Person>.Failure("Invalid type of member")
            };

            if (!personResult.IsSuccess)
            {
                return Result<string>.Failure(personResult.Error!);
            }

            LoginInfo loginInfo = new LoginInfo
            {
                Username = request.username,
                Password = request.password,
                Person = personResult.Value,
            };

            Person person = personResult.Value!;

            await this.personRepository.AddAsync(person);
            await this.loginInfoRepository.AddAsync(loginInfo);
            await this.unitOfWork.CompleteAsync();
            return Result<string>.Success(person.Id);
        }
    }
}
