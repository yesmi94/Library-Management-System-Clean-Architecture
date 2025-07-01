namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.DeleteBook
{
    using FluentValidation;

    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(book => book.BookId)
            .NotNull()
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Book ID must be a valid Guid");
        }
    }
}
