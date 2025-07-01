namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.DTO.PersonDTO;
    using LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson;
    using LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetMembers;
    using LibraryManagementCleanArchitecture.Application.UseCases.Persons.GetPersons;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class PersonEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/persons");

            group.MapGet("/", async (IMediator mediator) =>
            {
                var query = new GetPersonsQuery();
                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new() { result.Error! }, "Couldn't retrieve persons");
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<PersonDto>>.SuccessResponse(result.Value, "Succuessfully retrieved the persons list");
                Log.Information("Succuessfully retrieved the persons list");
                return Results.Ok(successResponse);
            });


            group.MapGet("/members", async (IMediator mediator) =>
            {
                var query = new GetMembersQuery();
                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new() { result.Error! }, "Couldn't retrieve members");
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<PersonDto>>.SuccessResponse(result.Value, "Succuessfully retrieved the members list");
                Log.Information("Succuessfully retrieved the members list");
                return Results.Ok(successResponse);
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreatePersonCommand command) =>
            {
                var result = await mediator.Send(command);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new() { result.Error! }, "Couldn't retrieve members");
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Person - {result.Value} was added successfully");
                Log.Information("Person - {personId} was added successfully", result.Value);
                return Results.Ok(successResponse);

            });
        }
    }
}
