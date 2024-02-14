namespace PdfGenerator.Models.Reports.EBoard;

public class DispatchSumResponse
{
    public DateTime Date { get; set; }
    public List<DispatchSumEmpRow> DispatchSumEmpRows { get; set; }
}