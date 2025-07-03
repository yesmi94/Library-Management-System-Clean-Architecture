// <copyright file="GetPersonsQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetPersons
{
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using MediatR;

    public record GetPersonsQuery(): IRequest<Result<List<PersonDto>>>;
}
