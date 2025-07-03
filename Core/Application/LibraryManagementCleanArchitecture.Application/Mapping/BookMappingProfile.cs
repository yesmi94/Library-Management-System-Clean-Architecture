// <copyright file="BookMappingProfile.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Mapping
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
    using LibraryManagementCleanArchitecture.Domain.Entities;

    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<Book, BookDto>();
            this.CreateMap<BookDto, Book>();
        }
    }
}
