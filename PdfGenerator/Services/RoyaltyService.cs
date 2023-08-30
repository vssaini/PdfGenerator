using Microsoft.Extensions.Logging;
using PdfGenerator.Components.Royalty;
using PdfGenerator.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

namespace PdfGenerator.Services;

public class RoyaltyService : IDocService
{
    private readonly ILogger<RoyaltyService> _logger;
    private readonly IRoyaltyDocDataSource _royDocDs;

    public RoyaltyService(ILogger<RoyaltyService> logger, IRoyaltyDocDataSource royDocDs)
    {
        _logger = logger;
        _royDocDs = royDocDs;
    }

    public void GenerateDoc(bool showInPreviewer = false, int fontSize = 8)
    {
        var model = _royDocDs.GetRoyaltyDetails();
        var document = new RoyaltyDocument(model, fontSize);

        if (showInPreviewer)
            document.ShowInPreviewer();
        else
            GeneratePdf(document, document.FilePath);
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        _logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);
        document.GeneratePdf(filePath);

        _logger.LogInformation("Opening PDF at {PdfFilePath}", filePath);
        Process.Start("explorer.exe", filePath);
    }
}