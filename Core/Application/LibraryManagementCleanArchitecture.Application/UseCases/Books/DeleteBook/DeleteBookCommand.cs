// <copyright file="DeleteBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.DeleteBook
{
    using MediatR;

    public record DeleteBookCommand(string bookId): IRequest<Result<string>>;
}
