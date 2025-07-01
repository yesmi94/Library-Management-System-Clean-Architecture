namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
    using LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook;
    using LibraryManagementCleanArchitecture.Application.UseCases.Books.DeleteBook;
    using LibraryManagementCleanArchitecture.Application.UseCases.Books.GetBooks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class BookEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/books");

            group.MapGet("/", async (IMediator mediator, string memberId) =>
            {
                var query = new GetBooksQuery(memberId);
                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't retrieve books");
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<BookDto>>.SuccessResponse(result.Value, "Books retrieved successfully");
                Log.Information("Books retrieved successfully", result);
                return Results.Ok(successResponse);
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateBookCommand command, [FromServices] IValidator<CreateBookCommand> validator) =>
            {
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(err => err.ErrorMessage);
                    return Results.BadRequest(errors);
                }

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new () { result.Error! }, "Couldn't create the new book");
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {result.Value} added successfully");
                Log.Information("Book - {bookId} added successfully", result.Value);
                return Results.Ok(successResponse);
            });



            group.MapDelete("/{bookNumber}", async (IMediator mediator, string bookNumber, [FromServices] IValidator<DeleteBookCommand> validator) =>
            {
                var command = new DeleteBookCommand(bookNumber);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.Select(error => error.ErrorMessage);
                    return Results.BadRequest(error);
                }

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], $"Couldn't delete the book with ID - {result.Value}");
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {result.Value} was deleted successfully");
                Log.Information("Book - {bookId} was deleted successfully", result.Value);
                return Results.Ok(successResponse);
            });
        }
    }
}
