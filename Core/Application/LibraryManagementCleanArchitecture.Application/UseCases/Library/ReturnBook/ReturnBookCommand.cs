// <copyright file="ReturnBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Library.ReturnBook
{
    using MediatR;

    public record ReturnBookCommand(string bookId, string personId): IRequest<Result<string>>;
}
