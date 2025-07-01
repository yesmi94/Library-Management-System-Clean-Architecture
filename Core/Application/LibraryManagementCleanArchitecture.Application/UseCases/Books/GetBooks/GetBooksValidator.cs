using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.GetBooks
{
    public class GetBooksValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksValidator()
        {
            RuleFor(book => book.personId)
                .NotNull()
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Person ID must be a valid Guid");
        }
    }
}
