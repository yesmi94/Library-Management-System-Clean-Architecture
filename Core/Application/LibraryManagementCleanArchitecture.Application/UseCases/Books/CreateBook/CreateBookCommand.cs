// <copyright file="CreateBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook
{
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public record CreateBookCommand
    (
        string title,
        string author,
        string year,
        BookCategory bookCategory) : IRequest<Result<string>>;
}
