using PdfGenerator.Models;
using PdfGenerator.Queries;

namespace PdfGenerator.Contracts;

public interface IRoyaltyRepo
{
    Task<List<RoyaltyResponse>> GetRoyaltiesAsync(GetRoyaltyQuery query);
}