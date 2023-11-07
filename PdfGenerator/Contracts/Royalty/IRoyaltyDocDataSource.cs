using PdfGenerator.Models.Royalty;
using PdfGenerator.Queries;

namespace PdfGenerator.Contracts.Royalty;

public interface IRoyaltyDocDataSource
{
    Task<RoyaltyModel> GetRoyaltyModelAsync(GetRoyaltyQuery query);
}