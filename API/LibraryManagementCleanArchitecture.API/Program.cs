
using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Persistance;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LibraryManagementCleanArchitecture.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<DataContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=LibraryDatabase;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlOptions => sqlOptions.MigrationsAssembly("LibraryManagementSystemEFCore.Infrastructure"))
            );

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();


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
