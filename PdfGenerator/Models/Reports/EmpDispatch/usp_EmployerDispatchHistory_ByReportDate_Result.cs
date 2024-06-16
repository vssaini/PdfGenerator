namespace PdfGenerator.Models.Reports.EmpDispatch;

public class usp_EmployerDispatchHistory_ByReportDate_Result
{
    public int EmployerID { get; set; }
    public string WorkerName { get; set; }
    public string Employer { get; set; }
    public string Location { get; set; }
    public string ShowName { get; set; }
    public int WorkerID { get; set; }
    public System.DateTime? DispatchedAt { get; set; }
    public System.DateTime? ReportAtTime { get; set; }
    public string ReportTime { get; set; }
    public int RequestID { get; set; }
    public string DispatchtDate { get; set; }
    public string Skill { get; set; }
    public System.DateTime? RD { get; set; }
}