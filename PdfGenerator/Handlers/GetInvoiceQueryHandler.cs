using MediatR;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Models.Invoice;
using PdfGenerator.Queries;

namespace PdfGenerator.Handlers;

internal sealed class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceModel>
{
    private readonly IInvoiceDocDataSource _invDocDs;

    public GetInvoiceQueryHandler(IInvoiceDocDataSource invDocDs)
    {
        _invDocDs = invDocDs;
    }

    public Task<InvoiceModel> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var model = _invDocDs.GetInvoiceModel();
        return Task.FromResult(model);
    }
}