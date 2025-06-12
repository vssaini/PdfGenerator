using PdfGenerator.Contracts;
using PdfGenerator.Models.Reports.Common;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Services.Helpers;

public class PdfInvoker
{
    public static async Task ShowOrPreviewPdfAsync(PdfFilter filter, ILogService logService, IDocument document)
    {
        var metadata = document.GetMetadata();

        if (filter.ShowPdfPreview)        
            await PreviewPdfAsync(logService, document, metadata.Title);        
        else        
            ShowGeneratedPdf(logService, document, metadata.Title);        
    }

    private static async Task PreviewPdfAsync(ILogService logService, IDocument document, string reportTitle)
    {
        logService.LogInformation("Showing {ReportName} PDF in QuestPDF Companion", reportTitle);
        await document.ShowInCompanionAsync();
    }

    private static void ShowGeneratedPdf(ILogService logService, IDocument document, string reportTitle)
    {
        logService.LogInformation("Generating and showing {ReportName} PDF", reportTitle);
        document.GeneratePdfAndShow();
    }
}