// <copyright file="LibraryEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook;
    using LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LibraryEndpoints : IEndpointGroup
    {
        private readonly ILogger<LibraryEndpoints> logger;

        public LibraryEndpoints(ILogger<LibraryEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/library");

            group.MapPut("/{bookId}/borrowings", [Authorize(Roles = "Member")] async ([FromBody] BorrowBookCommand command, IMediator mediator, [FromRoute] string bookId, [FromQuery] string personId, [FromServices] IValidator<BorrowBookCommand> validator) =>
            {
                command = new BorrowBookCommand(bookId, personId);

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<Book>.FailureResponse([result.Error!], "Couldn't borrow the book");
                    this.logger.LogWarning("Failed - Book with ID {bookId} borrowing failed", bookId);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<Book>.SuccessResponse(result.Value, $"Book - {bookId} borrowed by {personId} successfully. Your borrowing ID is {result.Value}");
                this.logger.LogInformation("Book - {bookId} borrowed by {personId} successfully. Borrowing ID id {Borrowing ID}", bookId, personId, result.Value);
                return Results.Ok(successResponse);
            });

            group.MapPut("/{borrowingId}/returnings", [Authorize(Roles = "Member")] async ([FromBody] ReturnBookCommand command, IMediator mediator, [FromRoute] string borrowingId, [FromQuery] string personId, [FromQuery] string bookId, [FromServices] IValidator<ReturnBookCommand> validator) =>
            {
                command = new ReturnBookCommand(bookId, personId, borrowingId);

                var result = await mediator.Send(command);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't return the book");
                    this.logger.LogWarning("Failed - Book with ID {bookId} returning failed.", bookId);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<Book>.SuccessResponse(result.Value, $"Book - {bookId} returned by {personId} successfully");
                this.logger.LogInformation("Book - {bookId} returned by {personId} successfully", bookId, personId);
                return Results.Ok(successResponse);
            });
        }
    }
}
