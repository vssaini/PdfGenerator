using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PdfGenerator.Components.Invoice;
using PdfGenerator.Contracts;
using PdfGenerator.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;
using PdfGenerator.Models;

namespace PdfGenerator.Services;

public class InvoiceService : IDocService
{
    private readonly ILogger<InvoiceService> _logger;
    private readonly ISender _sender;
    private readonly IConfiguration _config;

    public InvoiceService(ILogger<InvoiceService> logger, ISender sender, IConfiguration config)
    {
        _logger = logger;
        _sender = sender;
        _config = config;
    }

    public async Task GenerateDocAsync(DocFilter filter)
    {
        var model = await _sender.Send(new GetInvoiceQuery());

        var fontSize = _config.GetValue<int>("Pdf:FontSize");
        var document = new InvoiceDocument(model, fontSize);

        var showInPreviewer = _config.GetValue<bool>("Pdf:ShowInPreviewer");
        if (showInPreviewer)
            await document.ShowInPreviewerAsync();
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