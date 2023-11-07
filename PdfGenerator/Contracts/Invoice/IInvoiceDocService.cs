namespace PdfGenerator.Contracts.Invoice;

public interface IInvoiceDocService
{
    Task GenerateInvoiceDocAsync();
}