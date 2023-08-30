using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfGenerator.Contracts;
using PdfGenerator.Data;
using PdfGenerator.Services;
using Serilog;

namespace PdfGenerator;

internal static class Startup
{
    public static IHost ConfigureHostBuilder()
    {
        var builder = GetConfigBuilder();
        ConfigureLogger(builder);

        Log.Logger.Information("Starting PDF Generator application");

        return GetHost();
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

    private static void ConfigureLogger(ConfigurationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .CreateLogger();
    }

    private static IHost GetHost()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                //services.AddLogging();

                services.AddTransient<IDocService, RoyaltyService>();
                services.AddTransient<IPdfService, PdfService>();

                services.AddTransient<IInvoiceDocDataSource, InvoiceDocDataSource>();
                services.AddTransient<IRoyaltyDocDataSource, RoyaltyDocDataSource>();
            })
            .UseSerilog()
            .Build();

        return host;
    }
}