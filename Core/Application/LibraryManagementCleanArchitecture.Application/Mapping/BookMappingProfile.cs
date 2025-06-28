using AutoMapper;
using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
using LibraryManagementCleanArchitecture.Domain.Entities;

namespace LibraryManagementCleanArchitecture.Application.Mapping
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile() {

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}
