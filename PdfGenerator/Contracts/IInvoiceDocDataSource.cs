using PdfGenerator.Sample_Models;

namespace PdfGenerator.Contracts;

public interface IInvoiceDocDataSource
{
    InvoiceModel GetInvoiceDetails();
}