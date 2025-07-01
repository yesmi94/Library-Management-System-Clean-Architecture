
using FluentValidation;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator() {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title of the book is required");

            RuleFor(book => book.Author)
                .NotEmpty().WithMessage("Name of the author is required")
                .MaximumLength(100).WithMessage("Author's name cannot exceed 100 characters");

            RuleFor(book => book.Year)
                .NotEmpty().WithMessage("Publication year of the book is required")
                .Length(4).WithMessage("Year must contain 4 digits")
                .Matches(@"^\d{4}$").WithMessage("Year must contain only digits")
                .Must(year => int.Parse(year) <= DateTime.Now.Year).WithMessage("Publication year cannot be a future year");

            RuleFor(book => book.BookCategory)
                .NotNull().WithMessage("Book category must be specified")
                .IsInEnum();
        }
    }
}
