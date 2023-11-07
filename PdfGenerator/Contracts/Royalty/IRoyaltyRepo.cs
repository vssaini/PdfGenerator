using PdfGenerator.Models.Royalty;
using PdfGenerator.Queries;

namespace PdfGenerator.Contracts.Royalty;

public interface IRoyaltyRepo
{
    Task<List<RoyaltyResponse>> GetRoyaltiesAsync(GetRoyaltyQuery query);
}