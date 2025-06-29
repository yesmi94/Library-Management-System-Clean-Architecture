using AutoMapper;
using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.Queries
{
    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, List<PersonDto>>
    {
        private readonly IRepository<Person> personRepository;
        private readonly IMapper mapper;

        public GetMembersQueryHandler(IRepository<Person> personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }
        public async Task<List<PersonDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var persons = await personRepository.GetAllAsync();

            var members = new List<PersonDto>();

            if (persons.Count == 0)
            {
                throw new Exception("No people to display");
            }

            var personList = mapper.Map<List<PersonDto>>(persons);

            foreach (var personDto in personList) { 
                if(personDto.Role == UserType.Member)
                {
                    members.Add(personDto);
                }
            }

            return members;
        }
    }
}
