namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result<string>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        private readonly IUnitOfWork unitOfWork;

        public BorrowBookCommandHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.bookId);
            var person = await personRepository.GetByIdAsync(request.personId);

            if (book == null)
            {
                return Result<string>.Failure($"Failed: Book with {request.bookId} does not exist. Please check the book ID an try again");
            }

            if (person == null)
            {
                return Result<string>.Failure($"Failed: Couldn't find the person with ID - {request.personId}. Please check the ID and try again");
            }

            if (person.Role != UserType.Member)
            {
                return Result<string>.Failure("Only the members are allowed to borrow books. Minor staff and the Management staff cannot borrow books");
            }

            book.IsAvailable = false;
            person.BorrowedBooksNum++;

            await bookRepository.UpdateAsync(book);
            await personRepository.UpdateAsync(person);
            await unitOfWork.CompleteAsync();


            return Result<string>.Success(book.Id);
        }
    }
}
