using Microsoft.Extensions.DependencyInjection;
using PdfGenerator;
using PdfGenerator.Models;
using PdfGenerator.Services;
using Serilog;

var host = Startup.CreateHostBuilder();

var pdfSvc = ActivatorUtilities.CreateInstance<PdfService>(host.Services);
await pdfSvc.Run(new DocFilter(1997, 153043));

// Necessary; otherwise logs will not show in Seq
Log.CloseAndFlush();