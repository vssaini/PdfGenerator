namespace PdfGenerator.Models.Reports.EBoard;

public class usp_EBoard_DispatchSummary_Result
{
    public int RequestID { get; set; }
    public System.DateTime? ReportAtTime { get; set; }
    public string Employer { get; set; }
    public string Location { get; set; }
    public string LocationSub { get; set; }
    public int? Dispatches { get; set; }
    public string ShowName { get; set; }
}