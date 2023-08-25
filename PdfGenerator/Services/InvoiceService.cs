using PdfGenerator.Components.Invoice;
using PdfGenerator.Contracts;
using PdfGenerator.Data;
using QuestPDF.Previewer;

namespace PdfGenerator.Services;

public class InvoiceService : IDocService
{
    public void GenerateDoc(bool showInPreviewer = false, int fontSize = 8)
    {
        var model = InvoiceDocumentDataSource.GetInvoiceDetails();
        var document = new InvoiceDocument(model, fontSize);

        if (showInPreviewer)
            document.ShowInPreviewer();
        else
            PdfService.GeneratePdf(document, "invoice.pdf");
    }
}