using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PdfGenerator.Components.Royalty;
using PdfGenerator.Contracts;
using PdfGenerator.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;
using PdfGenerator.Models;

namespace PdfGenerator.Services;

public class RoyaltyService : IDocService
{
    private readonly ILogger<RoyaltyService> _logger;
    private readonly ISender _sender;
    private readonly IConfiguration _config;

    public RoyaltyService(ILogger<RoyaltyService> logger, ISender sender, IConfiguration config)
    {
        _logger = logger;
        _sender = sender;
        _config = config;
    }

    public async Task GenerateDocAsync()
    {
        var model = await _sender.Send(new GetRoyaltyQuery(new DocFilter(1997, 153043)));

        var fontSize = _config.GetValue<int>("Pdf:FontSize");
        var document = new RoyaltyDocument(model, fontSize);

        var showInPreviewer = _config.GetValue<bool>("Pdf:ShowInPreviewer");
        if (showInPreviewer)
            await document.ShowInPreviewerAsync();
        else
            GeneratePdf(document, document.FileName);
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        _logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);
        document.GeneratePdf(filePath);

        _logger.LogInformation("Opening PDF at {PdfFilePath}", filePath);
        Process.Start("explorer.exe", filePath);
    }
}