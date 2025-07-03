// <copyright file="DeleteBookCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.DeleteBook
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<string>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await this.bookRepository.GetByIdAsync(request.bookId);

            if (book == null)
            {
                return Result<string>.Failure("Failed: Trying to delete a book that does not exist. Please check the book ID and try again");
            }

            await this.bookRepository.DeleteAsync(book);
            await this.unitOfWork.CompleteAsync();

            return Result<string>.Success(book.Id);
        }
    }
}
