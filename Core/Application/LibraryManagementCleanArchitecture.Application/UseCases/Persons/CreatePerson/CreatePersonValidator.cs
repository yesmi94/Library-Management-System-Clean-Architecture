
namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    using FluentValidation;

    public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty().WithMessage("Name of the person is required")
                .MaximumLength(100).WithMessage("Name of the person cannot exceed 100 characters");
            RuleFor(person => person.Role)
                .NotNull().WithMessage("Role of the person cannot be empty")
                .IsInEnum();
            RuleFor(person => person.BorrowedBooksNum)
                .NotNull().WithMessage("Number of the borrowed books is required");
        }
    }
}
