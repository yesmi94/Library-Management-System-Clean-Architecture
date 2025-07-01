using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.UseCases.Persons.Commands;
using LibraryManagementCleanArchitecture.Application.UseCases.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public class PersonEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/persons");

            group.MapGet("/", async (IMediator mediator) =>
            {
                try
                {
                    var query = new GetPersonsQuery();
                    var persons = await mediator.Send(query);
                    Log.Information("Succuessfully retrieved the persons list");
                    return Results.Ok(persons);
                }
                catch (Exception exception)
                {
                    Log.Error(exception, exception.Message);
                    return Results.BadRequest(exception.Message);
                }
            });


            group.MapGet("/members", async (IMediator mediator) =>
            {
                try
                {
                    var query = new GetMembersQuery();
                    var members = await mediator.Send(query);
                    Log.Information("Succuessfully retrieved the members list");
                    return Results.Ok(members);
                }
                catch (Exception exception)
                {
                    Log.Error(exception, exception.Message);
                    return Results.BadRequest(exception.Message);
                }
            });

            
            group.MapPost("/", async (IMediator mediator, [FromBody] CreatePersonCommand command) =>
            {
                try
                {
                    var personId = await mediator.Send(command);
                    Log.Information("Person - {personId} was added successfully", personId);
                    return Results.Ok($"Person - {personId} was added successfully");
                }
                catch (Exception exception)
                {
                    Log.Error(exception, exception.Message);
                    return Results.BadRequest(exception.Message);
                }
            });
        }
    }
}
