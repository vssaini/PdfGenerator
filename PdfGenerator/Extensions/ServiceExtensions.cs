using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Contracts.Reports.Membership;
using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Data;
using PdfGenerator.Data.Reports.BaDispatch;
using PdfGenerator.Data.Reports.EBoard;
using PdfGenerator.Data.Reports.EmpDispatch;
using PdfGenerator.Data.Reports.Grievance;
using PdfGenerator.Data.Reports.Membership;
using PdfGenerator.Data.Reports.Request;
using PdfGenerator.Data.Royalty;
using PdfGenerator.Services;
using PdfGenerator.Services.Invoice;
using PdfGenerator.Services.Reports.BaDispatch;
using PdfGenerator.Services.Reports.EBoard;
using PdfGenerator.Services.Reports.EmpDispatch;
using PdfGenerator.Services.Reports.Grievance;
using PdfGenerator.Services.Reports.Membership;
using PdfGenerator.Services.Reports.Request;
using PdfGenerator.Services.Royalty;
using System.Reflection;

namespace PdfGenerator.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceDependencies(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("AdsConString");
        //var connectionString = config.GetConnectionString("UmgConString");

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        services.AddSingleton<ILogService, LogService>();

        services.AddUmgDependencies();
        services.AddAdsDependencies();
    }

    private static void AddUmgDependencies(this IServiceCollection services)
    {
        services.AddTransient<IInvoiceDocService, InvoiceService>();
        services.AddTransient<IInvoiceDocDataSource, InvoiceDocDataSource>();

        services.AddTransient<IRoyaltyDocService, RoyaltyService>();
        services.AddTransient<IRoyaltyDocDataSource, RoyaltyDocDataSource>();
        services.AddTransient<IRoyaltyRepo, RoyaltyRepo>();
    }

    private static void AddAdsDependencies(this IServiceCollection services)
    {
        services.AddTransient<IGrievanceDocService, GrievanceDocService>();
        services.AddTransient<IGrievanceDocDataSource, GrievanceDocDataSource>();
        services.AddTransient<IGrievanceRepo, GrievanceRepo>();

        services.AddTransient<IBaDispatchDocService, BaDispatchDocService>();
        services.AddTransient<IBaDispatchDocDataSource, BaDispatchDocDataSource>();
        services.AddTransient<IBaDispatchRepo, BaDispatchRepo>();

        services.AddTransient<IActiveMemberDocService, ActiveMemberDocService>();
        services.AddTransient<IActiveMemberDocDataSource, ActiveMemberDocDataSource>();
        services.AddTransient<IActiveMemberRepo, ActiveMemberRepo>();

        services.AddTransient<IDispatchWorkerListDocService, DispatchWorkerListDocService>();
        services.AddTransient<IDispatchWorkerListDocDataSource, DispatchWorkerListDocDataSource>();
        services.AddTransient<IDispatchWorkerListRepo, DispatchWorkerListRepo>();

        services.AddTransient<IDispatchSumDocService, DispatchSumDocService>();
        services.AddTransient<IDispatchSumDocDataSource, DispatchSumDocDataSource>();
        services.AddTransient<IDispatchSumRepo, DispatchSumRepo>();

        services.AddTransient<IEmpDispatchDocService, EmpDispatchDocService>();
        services.AddTransient<IEmpDispatchDocDataSource, EmpDispatchDocDataSource>();
        services.AddTransient<IEmpDispatchRepo, EmpDispatchRepo>();
    }
}