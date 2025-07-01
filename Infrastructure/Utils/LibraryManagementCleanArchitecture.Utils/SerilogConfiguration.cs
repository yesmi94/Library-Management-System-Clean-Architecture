namespace LibraryManagementCleanArchitecture.Utils
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(IHostBuilder hostBuilder, IConfiguration configuration)
        {
            hostBuilder.UseSerilog((context, services, loggerConfig) =>
            {
                loggerConfig
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341");
            });
        }
    }
}
