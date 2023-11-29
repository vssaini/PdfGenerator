using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Data;
using PdfGenerator.Data.Reports.BaDispatch;
using PdfGenerator.Data.Reports.Grievance;
using PdfGenerator.Data.Royalty;
using PdfGenerator.Services;
using PdfGenerator.Services.Reports.BaDispatch;
using PdfGenerator.Services.Reports.Grievance;
using Serilog;
using System.Reflection;
using PdfGenerator.Contracts.Membership;
using PdfGenerator.Data.Membership;
using PdfGenerator.Services.Reports.Membership;

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
        var connectionString = config.GetConnectionString("UnionManagerConString");
        //var connectionString = config.GetConnectionString("UmgConString");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

                services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
                services.AddSingleton<ILogService, LogService>();

                services.AddTransient<IInvoiceDocService, InvoiceService>();
                services.AddTransient<IInvoiceDocDataSource, InvoiceDocDataSource>();

                services.AddTransient<IRoyaltyDocService, RoyaltyService>();
                services.AddTransient<IRoyaltyDocDataSource, RoyaltyDocDataSource>();
                services.AddTransient<IRoyaltyRepo, RoyaltyRepo>();

                services.AddTransient<IGrievanceDocService, GrievanceDocService>();
                services.AddTransient<IGrievanceDocDataSource, GrievanceDocDataSource>();
                services.AddTransient<IGrievanceRepo, GrievanceRepo>();

                services.AddTransient<IBaDispatchDocService, BaDispatchDocService>();
                services.AddTransient<IBaDispatchDocDataSource, BaDispatchDocDataSource>();
                services.AddTransient<IBaDispatchRepo, BaDispatchRepo>();

                services.AddTransient<IActiveMemberDocService, ActiveMemberDocService>();
                services.AddTransient<IActiveMemberDocDataSource, ActiveMemberDocDataSource>();
                services.AddTransient<IActiveMemberRepo, ActiveMemberRepo>();
            })
            .UseSerilog()
            .Build();

        return host;
    }
}