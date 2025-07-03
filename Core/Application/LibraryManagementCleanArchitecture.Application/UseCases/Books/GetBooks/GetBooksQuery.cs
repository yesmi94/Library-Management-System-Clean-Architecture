// <copyright file="GetBooksQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Books.GetBooks
{
    using LibraryManagementCleanArchitecture.Application.DTO.BookDTO;
    using MediatR;

    public record GetBooksQuery(string personId): IRequest<Result<List<BookDto>>>;
}
