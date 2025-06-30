using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.Commands
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, string>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        private readonly IUnitOfWork unitOfWork;
        public ReturnBookCommandHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.bookId);
            var person = await personRepository.GetByIdAsync(request.personId);

            if (book == null)
                throw new BookNotFoundException($"Failed: Book with {request.bookId} does not exist. Please check the book ID an try again");

            if(person == null)
            {
                throw new InvalidPersonException($"Failed: Couldn't find the person with ID - {request.personId}. Please check the ID and try again");
            }

            if(person.Role != UserType.Member)
            {
                throw new InvalidPersonException("Only the members are allowed to return books");
            }

            book.IsAvailable = true;
            person.BorrowedBooksNum--;

            await bookRepository.UpdateAsync(book);
            await personRepository.UpdateAsync(person);
            await unitOfWork.CompleteAsync();

            return book.Id;

        }
    }
}
