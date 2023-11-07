namespace PdfGenerator.Models.Royalty;

public sealed class PeriodRow
{
    public Period Period { get; set; }
    public List<RoyaltyRow> Rows { get; set; } = new();
    public SubTotalPeriod SubTotalPeriod { get; set; }
}