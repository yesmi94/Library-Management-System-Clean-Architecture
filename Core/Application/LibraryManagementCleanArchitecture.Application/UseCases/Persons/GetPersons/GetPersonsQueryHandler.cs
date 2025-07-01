using AutoMapper;
using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.Queries
{
    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, List<PersonDto>>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IMapper mapper;

        public GetPersonsQueryHandler(IRepository<Person> personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public async Task<List<PersonDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await personRepository.GetAllAsync();

            if(persons.Count == 0)
            {
                throw new Exception("No people to display");
            }

            var personList = mapper.Map<List<PersonDto>>(persons);

            return personList;
        }
    }
}
