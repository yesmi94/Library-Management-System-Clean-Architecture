// <copyright file="PersonEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetMembers;
    using LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetPersons;
    using MediatR;

    public class PersonEndpoints : IEndpointGroup
    {
        private readonly ILogger<PersonEndpoints> logger;

        public PersonEndpoints(ILogger<PersonEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/persons");

            group.MapGet("/", async (IMediator mediator) =>
            {
                var query = new GetPersonsQuery();
                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't retrieve persons");
                    this.logger.LogWarning("Failed: Failed to retrieve the persons list. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<PersonDto>>.SuccessResponse(result.Value, "Succuessfully retrieved the persons list");
                this.logger.LogInformation("Succuessfully retrieved the persons list");
                return Results.Ok(successResponse);
            });

            group.MapGet("/members", async (IMediator mediator) =>
            {
                var query = new GetMembersQuery();
                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't retrieve members");
                    this.logger.LogWarning("Failed: Failed to retrieve the members list. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<PersonDto>>.SuccessResponse(result.Value, "Succuessfully retrieved the members list");
                this.logger.LogInformation("Succuessfully retrieved the members list");
                return Results.Ok(successResponse);
            });
        }
    }
}
