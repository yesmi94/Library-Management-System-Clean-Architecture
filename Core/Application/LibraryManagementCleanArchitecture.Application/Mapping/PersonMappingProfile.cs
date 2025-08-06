// <copyright file="PersonMappingProfile.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Mapping
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using LibraryManagementCleanArchitecture.Domain.Entities;

    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            this.CreateMap<Person, PersonDto>();
            this.CreateMap<PersonDto, Person>();
        }
    }
}
