using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.Commands
{
    public class CreateBookCommadHandler : IRequestHandler<CreateBookCommand, string>
    {

        private readonly IRepository<Book> bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateBookCommadHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Year = request.Year,
                Category = request.BookCategory,
                IsAvailable = true
            };

            await bookRepository.AddAsync(book);
            await unitOfWork.CompleteAsync();

            return book.Id;

        }
    }

}
