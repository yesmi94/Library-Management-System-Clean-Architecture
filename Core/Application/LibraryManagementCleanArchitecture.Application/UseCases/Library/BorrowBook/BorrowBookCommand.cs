// <copyright file="BorrowBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.BorrowBook
{
    using MediatR;

    public record BorrowBookCommand(string bookId, string personId): IRequest<Result<string>>;
}
