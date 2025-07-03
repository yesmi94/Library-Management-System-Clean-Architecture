// <copyright file="BorrowBookValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook
{
    using FluentValidation;

    public class BorrowBookValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookValidator()
        {
            this.RuleFor(book => book.bookId)
                .NotNull()
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Book ID must be a valid Guid");
            this.RuleFor(book => book.personId)
                .NotNull()
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Person ID must be a valid Guid");
        }
    }
}
