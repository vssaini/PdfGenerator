using PdfGenerator.Models;
using PdfGenerator.Queries;

namespace PdfGenerator.Contracts;

public interface IRoyaltyDocDataSource
{
    Task<RoyaltyModel> GetRoyaltyModelAsync(GetRoyaltyQuery query);
}