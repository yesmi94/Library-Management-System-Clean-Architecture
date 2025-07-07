namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.DTO.AuthDto;
    using LibraryManagementCleanArchitecture.Application.UseCases.Login;
    using LibraryManagementCleanArchitecture.Application.UseCases.Persons.CreatePerson;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class AuthEndpoints : IEndpointGroup
    {
        private readonly ILogger<AuthEndpoints> logger;

        public AuthEndpoints(ILogger<AuthEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (IMediator mediator, [FromBody] CreatePersonCommand command) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess) {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't retrieve members");
                    this.logger.LogWarning("Failed: Failed to create the new member. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value, $"Person - {result.Value} was added successfully");
                this.logger.LogInformation("Person - {personId} was added successfully", result.Value);
                return Results.Ok(successResponse);

            });

            app.MapPost("/login", async (IMediator mediator, [FromBody] PersonLoginCommand command) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], $"Couldn't retrieve the Token. Error: {result.Error}");
                    this.logger.LogWarning("Failed: Failed to retrieve the token. Error: {Error}", result.Error);
                    return Results.Json(response, statusCode: StatusCodes.Status401Unauthorized);

                }

                var successResponse = Response<LoginResponseDto>.SuccessResponse(result.Value, "Token retrieved successfully");
                this.logger.LogInformation("Token retrieved successfully.");
                return Results.Ok(successResponse);

            });

            app.MapGet("/whoami", [Authorize] (ClaimsPrincipal user) =>
            {
                var name = user.Identity?.Name;
                var role = user.FindFirst(ClaimTypes.Role)?.Value;
                return Results.Ok(new { name, role });
            });

            app.MapGet("/claims", [Authorize] (HttpContext context) =>
            {
                var claims = context.User.Claims.Select(c => new { c.Type, c.Value });
                return Results.Ok(claims);
            });


        }
    }
}
