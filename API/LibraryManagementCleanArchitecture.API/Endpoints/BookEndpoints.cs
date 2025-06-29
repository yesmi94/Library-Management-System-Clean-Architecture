using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.UseCases.Books.Commands;
using LibraryManagementCleanArchitecture.Application.UseCases.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public class BookEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/books");

            group.MapGet("/", async (IMediator mediator, string memberId) =>
            {
                try
                {
                    var query = new GetBooksQuery(memberId);
                    var books = await mediator.Send(query);
                    return Results.Ok(books);
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateBookCommand command) =>
            {
                try
                {
                    var bookId = await mediator.Send(command);
                    return Results.Ok($"Book - {bookId} added successfully");
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });

            group.MapDelete("/{bookNumber}", async (IMediator mediator, string bookNumber) =>
            {
                try
                {
                    var command = new DeleteBookCommand(bookNumber);
                    var bookId = await mediator.Send(command);
                    return Results.NoContent();
                }
                catch (Exception exception)
                {
                    return Results.NotFound(exception.Message);
                }
            });
        }
    }
}
