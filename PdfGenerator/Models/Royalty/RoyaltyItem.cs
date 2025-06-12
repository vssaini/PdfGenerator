namespace PdfGenerator.Models.Royalty;

public sealed class RoyaltyItem
{
    public string Country { get; set; }

    public List<PeriodRow> PeriodRows { get; set; } = new();
}