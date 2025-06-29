using LibraryManagementCleanArchitecture.Application.Exceptions;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, string>
    {

        private readonly IRepository<Book> bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.BookId);

            if (book == null)
                throw new BookNotFoundException("This book does not exist");

            await bookRepository.DeleteAsync(book);
            await unitOfWork.CompleteAsync();

            return book.Id;
        }
    }
}
