using PdfGenerator.Models.Royalty;

namespace PdfGenerator.Contracts.Royalty;

public interface IRoyaltyDocService
{
    Task GenerateRoyaltyDocAsync(RoyaltyFilter filter);
}