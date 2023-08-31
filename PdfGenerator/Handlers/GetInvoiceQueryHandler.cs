using MediatR;
using PdfGenerator.Contracts;
using PdfGenerator.Queries;
using PdfGenerator.Sample_Models;

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