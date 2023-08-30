using Microsoft.Extensions.Logging;
using PdfGenerator.Components.Invoice;
using PdfGenerator.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

namespace PdfGenerator.Services;

public class InvoiceService : IDocService
{
    private readonly ILogger<InvoiceService> _logger;
    private readonly IInvoiceDocDataSource _invDocDs;

    public InvoiceService(ILogger<InvoiceService> logger, IInvoiceDocDataSource invDocDs)
    {
        _logger = logger;
        _invDocDs = invDocDs;
    }

    public void GenerateDoc(bool showInPreviewer = false, int fontSize = 8)
    {
        var model = _invDocDs.GetInvoiceDetails();
        var document = new InvoiceDocument(model, fontSize);

        if (showInPreviewer)
            document.ShowInPreviewer();
        else
            GeneratePdf(document, "invoice.pdf");
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        _logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);

        document.GeneratePdf(filePath);
        Process.Start("explorer.exe", filePath);
    }
}