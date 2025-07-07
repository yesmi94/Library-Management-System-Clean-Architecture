// <copyright file="BookEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BookEndpoints : IEndpointGroup
    {
        private readonly ILogger<BookEndpoints> logger;

        public BookEndpoints(ILogger<BookEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/books");

            group.MapGet("/", [Authorize(Roles = "Member,ManagementStaff")] async (IMediator mediator, string memberId) =>
            {
                var query = new GetBooksQuery(memberId);
                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<List<BookDto>>.FailureResponse([result.Error!], "Couldn't retrieve books");
                    this.logger.LogWarning("Failed: Failed to retrieve books for {MemberId}. Error: {Error}", memberId, result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<BookDto>>.SuccessResponse(result.Value, "Books retrieved successfully");
                this.logger.LogInformation("Books retrieved successfully.");
                return Results.Ok(successResponse);
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateBookCommand command) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't create the new book");
                    this.logger.LogWarning("Failed: Failed to create the new book. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {result.Value} added successfully");
                this.logger.LogInformation("Book - {BookId} added successfully", result.Value);
                return Results.Ok(successResponse);
            });

            group.MapDelete("/{bookId}", async (IMediator mediator, string bookId) =>
            {
                var command = new DeleteBookCommand(bookId);

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], $"Couldn't delete the book with ID - {bookId}");
                    this.logger.LogWarning("Failed: Couldn't delete the book with ID: {BookId}. Error: {Error}", bookId, result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Book - {result.Value} was deleted successfully");
                this.logger.LogInformation("Book - {BookId} was deleted successfully", result.Value);
                return Results.Ok(successResponse);
            });
        }
    }
}
