namespace PdfGenerator.Models
{
    public sealed class RoyaltyItem
    {
        public string Country { get; set; }

        public List<PeriodRow> PeriodRows { get; set; }

        public RoyaltyItem()
        {
            PeriodRows = new List<PeriodRow>();
        }
    }
}
