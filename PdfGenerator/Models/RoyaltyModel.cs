namespace PdfGenerator.Models;

public sealed class RoyaltyModel
{
    public DateTime AsOfDate { get; set; }
    public DateTime RunDate { get; set; }

    public string StatementTitle { get; set; }
    public string StatementSubTitle { get; set; }
    public string PrintedFromTitle { get; set; }

    public string Artist { get; set; }
    public int Account { get; set; }

    public List<RoyaltyItem> Items { get; set; }

    public int Year { get; set; }
}