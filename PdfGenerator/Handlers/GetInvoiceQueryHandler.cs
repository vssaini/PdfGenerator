using MediatR;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Models.Invoice;
using PdfGenerator.Queries;

namespace PdfGenerator.Handlers;

internal sealed class GetInvoiceQueryHandler(IInvoiceDocDataSource invDocDs)
    : IRequestHandler<GetInvoiceQuery, InvoiceModel>
{
    public Task<InvoiceModel> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var model = invDocDs.GetInvoiceModel();
        return Task.FromResult(model);
    }
}