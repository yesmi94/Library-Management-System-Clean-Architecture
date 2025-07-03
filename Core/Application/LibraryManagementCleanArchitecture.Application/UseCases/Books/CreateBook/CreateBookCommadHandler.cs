// <copyright file="CreateBookCommadHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public class CreateBookCommadHandler : IRequestHandler<CreateBookCommand, Result<string>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateBookCommadHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.title,
                Author = request.author,
                Year = request.year,
                Category = request.bookCategory,
                IsAvailable = true,
            };

            await this.bookRepository.AddAsync(book);
            await this.unitOfWork.CompleteAsync();

            return Result<string>.Success(book.Id);
        }
    }
}
