using PdfGenerator.Models;

namespace PdfGenerator.Contracts;

public interface IRoyaltyDocDataSource
{
    RoyaltyModel GetRoyaltyDetails();
}