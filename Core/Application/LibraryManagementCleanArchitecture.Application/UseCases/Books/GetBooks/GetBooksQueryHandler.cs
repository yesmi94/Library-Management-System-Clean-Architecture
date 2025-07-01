namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.GetBooks
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<List<BookDto>>>
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

        public async Task<Result<List<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var person = await personRepository.GetByIdAsync(request.personId);

            if (person == null)
            {
                return Result<List<BookDto>>.Failure($"Failed: Couldn't find the person with ID - {request.personId}. Please check the ID and try again");
            }

            if (person.Role == UserType.MinorStaff)
            {
                return Result<List<BookDto>>.Failure("Failed: Minor staff is not allowed to view the book list");
            }

            var books = await bookRepository.GetAllAsync();

            var bookList = mapper.Map<List<BookDto>>(books);

            return Result<List<BookDto>>.Success(bookList);
        }
    }
}
