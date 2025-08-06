// <copyright file="CreatePersonValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    using FluentValidation;

    public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonValidator()
        {
            this.RuleFor(person => person.name)
                .NotEmpty().WithMessage("Name of the person is required")
                .MaximumLength(100).WithMessage("Name of the person cannot exceed 100 characters");
            this.RuleFor(person => person.role)
                .NotNull().WithMessage("Role of the person cannot be empty")
                .IsInEnum();
            this.RuleFor(person => person.borrowedBooksNum)
                .NotNull().WithMessage("Number of the borrowed books is required");
        }
    }
}
