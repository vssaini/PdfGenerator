using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Grievance;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Data;
using PdfGenerator.Data.Grievance;
using PdfGenerator.Data.Royalty;
using PdfGenerator.Services;
using PdfGenerator.Services.Grievance;
using Serilog;
using System.Reflection;

namespace PdfGenerator;

internal static class Startup
{
    public static IHost CreateHostBuilder()
    {
        var builder = GetConfigBuilder();
        var configuration = builder.Build();

        ConfigureLogger(configuration);

        var appName = configuration.GetSection("ApplicationName").Value;
        Log.Logger.Information("Starting {ApplicationName} application", appName);

        return GetHost(configuration);
    }

    private static ConfigurationBuilder GetConfigBuilder()
    {
        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();

        return builder;
    }

    private static void ConfigureLogger(IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();
    }

    private static IHost GetHost(IConfiguration config)
    {
        //var connectionString = config.GetConnectionString("UnionManagerConString");
        var connectionString = config.GetConnectionString("UmgConString");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

                services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

                services.AddTransient<IInvoiceDocService, InvoiceService>();
                services.AddTransient<IInvoiceDocDataSource, InvoiceDocDataSource>();

                services.AddTransient<IRoyaltyDocService, RoyaltyService>();
                services.AddTransient<IRoyaltyDocDataSource, RoyaltyDocDataSource>();
                services.AddTransient<IRoyaltyRepo, RoyaltyRepo>();

                services.AddTransient<IGrievanceDocService, GrievanceStepOneDocService>();
                services.AddTransient<IGrievanceDocDataSource, GrievanceDocDataSource>();
                services.AddTransient<IGrievanceRepo, GrievanceRepo>();
            })
            .UseSerilog()
            .Build();

        return host;
    }
}