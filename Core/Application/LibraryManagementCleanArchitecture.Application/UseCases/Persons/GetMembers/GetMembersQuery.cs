// <copyright file="GetMembersQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetMembers
{
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using MediatR;

    public record GetMembersQuery(): IRequest<Result<List<PersonDto>>>;
}
