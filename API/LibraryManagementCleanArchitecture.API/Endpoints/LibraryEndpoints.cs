namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook;
    using LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class LibraryEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/library");

            group.MapPut("/{bookId}/borrowings", async([FromBody] BorrowBookCommand command, IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId, [FromServices] IValidator<BorrowBookCommand> validator) =>
            {
                command = new BorrowBookCommand(bookId, personId);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.Select(err => err.ErrorMessage);
                    return Results.BadRequest(error);
                }

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't borrow the book");
                    Log.Error("Failed - Book with ID {bookId} borrowing failed", bookId);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {bookId} borrowed by {personId} successfully");
                Log.Information("Book - {bookId} borrowed by {personId} successfully", bookId);
                return Results.Ok(successResponse);
            });



            group.MapPut("/{bookId}/returnings", async ([FromBody] ReturnBookCommand command, IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId, [FromServices] IValidator<ReturnBookCommand> validator) =>
            {
                command = new ReturnBookCommand(bookId, personId);
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.Select(err => err.ErrorMessage);
                    return Results.BadRequest(error);
                }

                var result = await mediator.Send(command);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't return the book");
                    Log.Error("Failed - Book with ID {bookId} returning failed", bookId);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {bookId} returned by {personId} successfully");
                Log.Information("Book - {bookId} returned by {personId} successfully", bookId);
                return Results.Ok(successResponse);
            });
        }
    }
}
