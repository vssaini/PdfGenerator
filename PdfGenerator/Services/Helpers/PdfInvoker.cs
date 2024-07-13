using PdfGenerator.Contracts;
using PdfGenerator.Models.Reports.Common;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Helpers;

public class PdfInvoker
{
    public static async Task ShowOrPreviewPdfAsync(PdfFilter filter, ILogService logService, IDocument document)
    {
        var metadata = document.GetMetadata();

        if (filter.ShowPdfPreview)
        {
            logService.LogInformation("Showing {ReportName} PDF in Previewer", metadata.Title);
            await document.ShowInPreviewerAsync();
        }
        else
        {
            logService.LogInformation("Generating and showing {ReportName} PDF", metadata.Title);
            document.GeneratePdfAndShow();
        }
    }
}