using Microsoft.Extensions.DependencyInjection;
using PdfGenerator;
using PdfGenerator.Models.Enums;
using PdfGenerator.Services;
using Serilog;

try
{
    var host = Startup.CreateHostBuilder();

    var pdfSvc = ActivatorUtilities.CreateInstance<PdfService>(host.Services);
    await pdfSvc.Run(Document.BaDispatch);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Log.Fatal(ex, "An unhandled exception occurred");
    Console.ResetColor();
}
finally
{
    // Necessary; otherwise logs will not show in Seq
    Log.CloseAndFlush();
}