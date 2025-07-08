// <copyright file="ReturnBookCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook
{
    using LibraryManagementCleanArchitecture.Application.Exceptions;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result<Book>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<Borrowing> borrowingRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookCommandHandler(IRepository<Book> bookRepository, IRepository<Person> personRepository, IUnitOfWork unitOfWork, IRepository<Borrowing> borrowingRepository)
        {
            this.bookRepository = bookRepository;
            this.personRepository = personRepository;
            this.borrowingRepository = borrowingRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Book>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var borrowing = await this.borrowingRepository.GetByIdAsync(request.borrowingId);
            var bookId = await this.bookRepository.GetByIdAsync(borrowing.BookId);
            var book = await this.bookRepository.GetByIdAsync(request.bookId);
            var person = await this.personRepository.GetByIdAsync(request.personId);

            if (borrowing == null || borrowing.IsReturned)
            {
                return Result<Book>.Failure("Invalid borrowing record.");
            }

            borrowing.IsReturned = true;

            book = borrowing.Book;
            book.IsAvailable = true;

            if (person.Role != UserType.Member)
            {
                return Result<Book>.Failure("Only the members are allowed to return books");
            }

            book.IsAvailable = true;
            person.BorrowedBooksNum--;

            await this.bookRepository.UpdateAsync(book);
            await this.personRepository.UpdateAsync(person);
            await this.unitOfWork.CompleteAsync();

            return Result<Book>.Success(book);
        }
    }
}
