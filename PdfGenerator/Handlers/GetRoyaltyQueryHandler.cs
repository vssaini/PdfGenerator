using MediatR;
using PdfGenerator.Contracts;
using PdfGenerator.Models;
using PdfGenerator.Queries;

namespace PdfGenerator.Handlers;

internal sealed class GetRoyaltyQueryHandler : IRequestHandler<GetRoyaltyQuery, RoyaltyModel>
{
    private readonly IRoyaltyDocDataSource _royDocDs;

    public GetRoyaltyQueryHandler(IRoyaltyDocDataSource royDocDs)
    {
        _royDocDs = royDocDs;
    }

    public async Task<RoyaltyModel> Handle(GetRoyaltyQuery request, CancellationToken cancellationToken)
    {
        return await _royDocDs.GetRoyaltyModelAsync(request);
    }
}