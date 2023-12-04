namespace PdfGenerator.Models.Reports.BaDispatch;

public class vw_BADispatchReport_Sub
{
    public int RequestID { get; set; }
    internal string ReportTime { get; set; }
    public DateTime ReportAtTime => DateTime.TryParse(ReportTime, out var reportTime) ? reportTime : DateTime.MinValue;
    public string Skill { get; set; }
    public int WorkerID { get; set; }
    public string _LastFirst { get; set; }
    public bool CallByName { get; set; }
    public string WorkerStatus { get; set; }
}