// <copyright file="DeleteBookValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.DeleteBook
{
    using FluentValidation;

    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            this.RuleFor(book => book.bookId)
            .NotNull()
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Book ID must be a valid Guid");
        }
    }
}
