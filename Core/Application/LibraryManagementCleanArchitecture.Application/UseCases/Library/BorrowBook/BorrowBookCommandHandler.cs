// <copyright file="BorrowBookCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result<Book>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<Borrowing> borrowingRepository;
        private readonly IUnitOfWork unitOfWork;

        public BorrowBookCommandHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository, IUnitOfWork unitOfWork, IRepository<Borrowing> borrowingRepository)
        {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;
            this.unitOfWork = unitOfWork;
            this.borrowingRepository = borrowingRepository;
        }

        public async Task<Result<Book>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await this.bookRepository.GetByIdAsync(request.bookId);
            var person = await this.personRepository.GetByIdAsync(request.personId);

            if (book == null)
            {
                return Result<Book>.Failure($"Failed: Book with {request.bookId} does not exist. Please check the book ID an try again");
            }

            if (person == null)
            {
                return Result<Book>.Failure($"Failed: Couldn't find the person with ID - {request.personId}. Please check the ID and try again");
            }

            if (!book.IsAvailable)
            {
                return Result<Book>.Failure("Book is currently unavailable");
            }

            /*var existing = await this.borrowingRepository.FindAsync(b =>
                b.BookId == request.bookId &&
                b.MemberId == request.personId &&
                b.IsReturned == book.IsAvailable && b.IsReturned == false);

            if (existing != null)
            {
                return Result<string>.Failure("This book is already borrowed by this member and not yet returned.");
            }*/


            if (person.Role != UserType.Member)
            {
                return Result<Book>.Failure("Only the members are allowed to borrow books. Minor staff and the Management staff cannot borrow books");
            }

            book.IsAvailable = false;
            person.BorrowedBooksNum++;

            Borrowing borrowing = new Borrowing
            {
                Person = person,
                Book = book,
                IsReturned = book.IsAvailable,
                MemberId = request.personId,
                BookId = request.bookId,

            };

            await this.bookRepository.UpdateAsync(book);
            await this.borrowingRepository.AddAsync(borrowing);
            await this.unitOfWork.CompleteAsync();

            return Result<Book>.Success(book);
        }
    }
}
