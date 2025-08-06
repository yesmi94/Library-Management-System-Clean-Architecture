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
    using LibraryManagementCleanArchitecture.Persistance.Identity;
    using LibraryManagementCleanArchitecture.Utils;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Serilog;
    using System.Security.Claims;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Host.UseSerilog();

            SerilogConfiguration.ConfigureSerilog(builder.Host, builder.Configuration);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                };

            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library API",
                    Version = "v1",
                });

                // Add JWT bearer auth definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your Token",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
    });

            });

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
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
            builder.Services.AddScoped<BookEndpoints>();
            builder.Services.AddScoped<LibraryEndpoints>();
            builder.Services.AddScoped<PersonEndpoints>();
            builder.Services.AddScoped<AuthEndpoints>();

            builder.Services.AddDbContext<DataContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=LibraryDatabase;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlOptions => sqlOptions.MigrationsAssembly("LibraryManagementCleanArchitecture.Persistance")));

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

            /*Global Exception Handler*/
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
