using Microsoft.Extensions.DependencyInjection;
using PdfGenerator;
using PdfGenerator.Models.Enums;
using PdfGenerator.Services;
using Serilog;

var host = Startup.CreateHostBuilder();

var pdfSvc = ActivatorUtilities.CreateInstance<PdfService>(host.Services);
await pdfSvc.Run(Document.ActiveMember);

// Necessary; otherwise logs will not show in Seq
Log.CloseAndFlush();