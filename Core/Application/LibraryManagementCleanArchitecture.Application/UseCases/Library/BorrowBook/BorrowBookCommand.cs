// <copyright file="BorrowBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public record BorrowBookCommand(string bookId, string personId, bool isReturned = false) : IRequest<Result<Book>>;
}
