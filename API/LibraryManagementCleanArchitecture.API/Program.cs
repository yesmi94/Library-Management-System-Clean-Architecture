// <copyright file="Program.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.API.Endpoints;
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.API.Middlewares;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.Behaviors;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook;
    using LibraryManagementCleanArchitecture.Persistance;
    using LibraryManagementCleanArchitecture.Utils;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Information("Startup test log — should appear in Seq.");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            SerilogConfiguration.ConfigureSerilog(builder.Host, builder.Configuration);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            /*Logging pipeline*/
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));

            /*Validation pipeline*/
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<BookEndpoints>();
            builder.Services.AddScoped<LibraryEndpoints>();
            builder.Services.AddScoped<PersonEndpoints>();

            builder.Services.AddDbContext<DataContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=LibraryDatabase;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlOptions => sqlOptions.MigrationsAssembly("LibraryManagementSystemEFCore.Infrastructure")));

            var app = builder.Build();

            app.RegisterAllEndpointGroups();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            /*Global Exception Handler*/
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}
