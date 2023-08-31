namespace PdfGenerator.Models;

public sealed class RoyaltyResponse
{
    public int Id { get; init; }

    public string Frekno { get; init; }

    public int AccountNumber { get; init; }

    public string StmtCycle { get; init; }

    public string Artist { get; init; }

    public string Country { get; init; }

    public DateTime FromDate { get; init; }

    public DateTime ToDate { get; init; }

    public int Units { get; init; }

    public decimal RoyaltyAmount { get; init; }

    public double AfmLia { get; init; }

    public decimal RoyBase { get; init; }

    public decimal RoyRate { get; init; }

    public double LicPercentage { get; init; }

    public int TotalRows { get; set; }
}