using PdfGenerator.Models.Invoice;

namespace PdfGenerator.Contracts.Invoice;

public interface IInvoiceDocDataSource
{
    InvoiceModel GetInvoiceModel();
}