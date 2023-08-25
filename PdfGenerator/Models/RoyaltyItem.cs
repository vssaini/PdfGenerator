namespace PdfGenerator.Models
{
    public sealed class RoyaltyItem
    {
        public string Country { get; set; }
        public Period Period { get; set; }
        public List<RoyaltyRow> Rows { get; set; } = new();
        public SubTotalPeriod SubTotalPeriod { get; set; }
    }
}
