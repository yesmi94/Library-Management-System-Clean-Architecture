
namespace LibraryManagementCleanArchitecture.API
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application;
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Application.UseCases.Books.CreateBook;
    using LibraryManagementCleanArchitecture.Persistance;
    using LibraryManagementCleanArchitecture.Utils;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<DataContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=LibraryDatabase;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlOptions => sqlOptions.MigrationsAssembly("LibraryManagementSystemEFCore.Infrastructure"))
            );

            SerilogConfiguration.ConfigureSerilog(builder.Host, builder.Configuration);


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

            app.Run();
        }
    }
}
