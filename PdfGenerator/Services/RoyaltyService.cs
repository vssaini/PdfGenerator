using PdfGenerator.Components.Royalty;
using PdfGenerator.Contracts;
using PdfGenerator.Data;
using QuestPDF.Previewer;

namespace PdfGenerator.Services;

public class RoyaltyService : IDocService
{
    public void GenerateDoc(bool showInPreviewer = false, int fontSize = 8)
    {
        var source = new RoyaltyDocumentDataSource();
        var model = source.GetRoyaltyDetails();

        var document = new RoyaltyDocument(model, fontSize);

        if (showInPreviewer)
            document.ShowInPreviewer();
        else
            PdfService.GeneratePdf(document, document.FilePath);
    }
}