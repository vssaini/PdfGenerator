namespace PdfGenerator.Models.Reports.EBoard;

public class DispatchSumResponse
{
    public string Employer { get; set; }
    public List<DispatchSumRow> SummaryRows { get; set; }
}