using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

namespace PdfGenerator.Services.Invoice;

public class InvoiceService(ILogger<InvoiceService> logger, ISender sender, IConfiguration config)
    : IInvoiceDocService, IPdfService
{
    public async Task GenerateInvoiceDocAsync()
    {
        logger.LogInformation("Generating Invoice document");

        var model = await sender.Send(new GetInvoiceQuery());

        var fontSize = config.GetValue<int>("Pdf:FontSize");
        var document = new InvoiceDocument(model, fontSize);

        var showInPreviewer = config.GetValue<bool>("Pdf:ShowInPreviewer");
        if (showInPreviewer)
            await document.ShowInPreviewerAsync();
        else
            GeneratePdf(document, "invoice.pdf");
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);

        document.GeneratePdf(filePath);
        Process.Start("explorer.exe", filePath);
    }
}