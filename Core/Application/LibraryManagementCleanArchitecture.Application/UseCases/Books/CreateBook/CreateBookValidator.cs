// <copyright file="CreateBookValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook
{
    using FluentValidation;

    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            this.RuleFor(book => book.title)
                .NotEmpty().WithMessage("Title of the book is required");

            this.RuleFor(book => book.author)
                .NotEmpty().WithMessage("Name of the author is required")
                .MaximumLength(100).WithMessage("Author's name cannot exceed 100 characters");

            this.RuleFor(book => book.year)
                .NotEmpty().WithMessage("Publication year of the book is required")
                .Length(4).WithMessage("Year must contain 4 digits")
                .Matches(@"^\d{4}$").WithMessage("Year must contain only digits")
                .Must(year => int.Parse(year) <= DateTime.Now.Year).WithMessage("Publication year cannot be a future year");

            this.RuleFor(book => book.bookCategory)
                .NotNull().WithMessage("Book category must be specified")
                .IsInEnum();
        }
    }
}
