using AutoMapper;
using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        private readonly IMapper mapper;

        public GetBooksQueryHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;  
            this.mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var person = await personRepository.GetByIdAsync(request.personId);

            if (person == null)
            {
                throw new InvalidPersonException("Please enter a valid ID");
            }

            if (person.Role == UserType.MinorStaff)
            {
                throw new InvalidPersonException("Minor staff cannot view the book list");
            }

            var books = await bookRepository.GetAllAsync();

            var bookList = mapper.Map<List<BookDto>>(books);

            return bookList;
        }
    }
}
