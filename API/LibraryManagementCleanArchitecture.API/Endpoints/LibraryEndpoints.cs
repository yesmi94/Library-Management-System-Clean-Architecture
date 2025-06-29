using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.UseCases.Library.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public class LibraryEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/library");

            group.MapPost("/{bookId}/borrowings", async(IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId) =>
            {
                try
                {
                    await mediator.Send(new BorrowBookCommand(bookId, personId));
                    return Results.Ok($"Book {bookId} borrowed by {personId} successfully");
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });

            group.MapPost("/{bookId}/returnings", async (IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId) =>
            {
                try
                {
                    await mediator.Send(new ReturnBookCommand(bookId, personId));
                    return Results.Ok($"Book {bookId} returned by {personId} successfully");
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });
        }
    }
}
