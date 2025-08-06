// <copyright file="ReturnBookValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook
{
    using FluentValidation;

    public class ReturnBookValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookValidator()
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
