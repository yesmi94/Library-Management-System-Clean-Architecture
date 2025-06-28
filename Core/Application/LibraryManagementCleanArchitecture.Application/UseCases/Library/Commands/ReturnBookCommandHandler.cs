using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.Commands
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, string>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        public ReturnBookCommandHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository) {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;
        }
        public async Task<string> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.bookId);
            var person = await personRepository.GetByIdAsync(request.personId);

            if (book == null)
                throw new BookNotFoundException("This is not a valid book");

            if(person == null)
            {
                throw new InvalidPersonException("Please enter a valid ID");
            }

            if(person.Role != UserType.Member)
            {
                throw new InvalidPersonException("Only the members are allowed to return books");
            }

            book.IsAvailable = true;

            return book.Id;

        }
    }
}
