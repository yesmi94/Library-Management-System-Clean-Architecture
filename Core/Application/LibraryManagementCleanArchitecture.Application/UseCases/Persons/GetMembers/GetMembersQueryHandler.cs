// <copyright file="GetMembersQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetMembers
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, Result<List<PersonDto>>>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IMapper mapper;

        public GetMembersQueryHandler(IRepository<Person> personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<PersonDto>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var persons = await this.personRepository.GetAllAsync();

            var members = new List<PersonDto>();

            if (persons.Count == 0)
            {
                return Result<List<PersonDto>>.Failure("No people to display");
            }

            var personList = this.mapper.Map<List<PersonDto>>(persons);

            foreach (var personDto in personList)
            {
                if (personDto.Role == UserType.Member)
                {
                    members.Add(personDto);
                }
            }

            return Result<List<PersonDto>>.Success(members);
        }
    }
}
