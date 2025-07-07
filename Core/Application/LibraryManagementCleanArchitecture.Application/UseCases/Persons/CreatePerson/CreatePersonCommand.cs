// <copyright file="CreatePersonCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson
{
    using MediatR;
    using static LibraryManagementCleanArchitecture.Domain.Enums.Enums;

    public record CreatePersonCommand(
        string name,
        UserType role,
        int borrowedBooksNum,
        string username,
        string password) : IRequest<Result<string>>;
}
