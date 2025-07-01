namespace LibraryManagementCleanArchitecture.API.Endpoints
{
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

            group.MapPost("/{bookId}/borrowings", async(IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId) =>
            {
                var result = await mediator.Send(new BorrowBookCommand(bookId, personId));
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new() { result.Error! }, "Couldn't borrow the book");
                    Log.Error("Failed - Book with ID {bookId} borrowing failed", bookId);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {bookId} borrowed by {personId} successfully");
                Log.Information("Book - {bookId} borrowed by {personId} successfully", bookId);
                return Results.Ok(successResponse);

            });

            group.MapPost("/{bookId}/returnings", async (IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId) =>
            {
                var result = await mediator.Send(new ReturnBookCommand(bookId, personId));
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new() { result.Error! }, "Couldn't return the book");
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
