using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.UseCases.Persons.Commands;
using LibraryManagementCleanArchitecture.Application.UseCases.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
                    return Results.Ok(persons);
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });


            group.MapGet("/members", async (IMediator mediator) =>
            {
                try
                {
                    var query = new GetMembersQuery();
                    var members = await mediator.Send(query);
                    return Results.Ok(members);
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });

            
            group.MapPost("/", async (IMediator mediator, [FromBody] CreatePersonCommand command) =>
            {
                try
                {
                    var personId = await mediator.Send(command);
                    return Results.Ok($"Person - {personId} was added successfully");
                }
                catch (Exception exception)
                {
                    return Results.BadRequest(exception.Message);
                }
            });
        }
    }
}
