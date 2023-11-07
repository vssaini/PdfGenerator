namespace PdfGenerator.Models.Royalty;

/// <summary>
/// Represents the row of royalty starting from Catalog number.
/// </summary>
public sealed class RoyaltyRow
{
    public string CatalogNumber { get; set; }
    public int Units { get; set; }
    public decimal Rate { get; set; }

    public decimal Amount { get; set; }
    public string Cycle { get; set; }
    public double AfmLia { get; set; }
    public decimal RoyaltyBase { get; set; }

    public decimal RoyaltyRate { get; set; }
    public string Type { get; set; }
    public double LicPercentage { get; set; }
}