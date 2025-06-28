using AutoMapper;
using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
using LibraryManagementCleanArchitecture.Domain.Entities;

namespace LibraryManagementCleanArchitecture.Application.Mapping
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile() {

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
