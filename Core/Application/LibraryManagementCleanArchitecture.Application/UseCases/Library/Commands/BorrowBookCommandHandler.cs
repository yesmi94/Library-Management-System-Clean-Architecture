using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementSystemEFCore.Domain.Enums.Enums;
namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.Commands
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, string>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        public BorrowBookCommandHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository)
        {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;
        }
        public async Task<string> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.bookId);
            var person = await personRepository.GetByIdAsync(request.perosnId);

            if (book == null)
                throw new BookNotFoundException("This book does not exist");

            if (person == null)
            {
                throw new InvalidPersonException("Please enter a valid ID");
            }

            if (person.Role != UserType.Member)
            {
                throw new InvalidPersonException("Only the members are allowed to borrow books");
            }

            book.IsAvailable = false;

            await bookRepository.Update(book);

            return book.Id;
        }
    }
}
