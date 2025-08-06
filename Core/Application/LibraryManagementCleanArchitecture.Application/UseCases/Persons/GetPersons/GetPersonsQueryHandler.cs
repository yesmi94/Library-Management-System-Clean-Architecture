// <copyright file="GetPersonsQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetPersons
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, Result<List<PersonDto>>>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IMapper mapper;

        public GetPersonsQueryHandler(IRepository<Person> personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<PersonDto>>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await this.personRepository.GetAllAsync();

            if (persons.Count == 0)
            {
                return Result<List<PersonDto>>.Failure("No people to display");
            }

            var personList = this.mapper.Map<List<PersonDto>>(persons);

            return Result<List<PersonDto>>.Success(personList);
        }
    }
}
