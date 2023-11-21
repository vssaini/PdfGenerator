namespace PdfGenerator.Models.Reports.BaDispatch;

public class vw_BADispatchReport_Sub
{
    public int RequestID { get; set; }
    public string ReportTime { get; set; }
    public string Skill { get; set; }
    public int WorkerID { get; set; }
    public string C_LastFirst { get; set; }
    public bool CallByName { get; set; }
    public string WorkerStatus { get; set; }
}