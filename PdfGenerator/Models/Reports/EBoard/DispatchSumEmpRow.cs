namespace PdfGenerator.Models.Reports.EBoard;

public class DispatchSumEmpRow
{
    public string Employer { get; set; }
    public List<DispatchSumRow> SummaryRows { get; set; }
}