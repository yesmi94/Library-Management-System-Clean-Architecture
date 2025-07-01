
namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook
{
    using FluentValidation;

    public class ReturnBookValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookValidator()
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
