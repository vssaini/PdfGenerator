using PdfGenerator.Components.Royalty;
using PdfGenerator.Data;

namespace PdfGenerator.Services;

public class RoyaltyService
{
    public static RoyaltyDocument GenerateRoyaltyDoc()
    {
        var source = new RoyaltyDocumentDataSource();
        var model = source.GetRoyaltyDetails();

        return new RoyaltyDocument(model);
    }
}