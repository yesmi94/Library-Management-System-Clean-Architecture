namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook
{
    using FluentValidation;

    public class BorrowBookValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookValidator()
        {
            RuleFor(book => book.bookId)
                .NotNull()
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Book ID must be a valid Guid");
            RuleFor(book => book.personId)
                .NotNull()
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Person ID must be a valid Guid");
        }
    }
}
