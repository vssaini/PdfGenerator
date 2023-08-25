using PdfGenerator.Components.Invoice;
using PdfGenerator.Data;

namespace PdfGenerator.Services;

public class InvoiceService
{
    public static InvoiceDocument GenerateInvoiceDoc()
    {
        var model = InvoiceDocumentDataSource.GetInvoiceDetails();
        return new InvoiceDocument(model);
    }
}