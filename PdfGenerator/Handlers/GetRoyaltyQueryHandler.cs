using MediatR;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Models.Royalty;
using PdfGenerator.Queries;

namespace PdfGenerator.Handlers;

internal sealed class GetRoyaltyQueryHandler(IRoyaltyDocDataSource royDocDs)
    : IRequestHandler<GetRoyaltyQuery, RoyaltyModel>
{
    public async Task<RoyaltyModel> Handle(GetRoyaltyQuery request, CancellationToken cancellationToken)
    {
        return await royDocDs.GetRoyaltyModelAsync(request);
    }
}