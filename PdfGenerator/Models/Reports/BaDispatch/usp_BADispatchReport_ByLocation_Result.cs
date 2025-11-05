namespace PdfGenerator.Models.Reports.BaDispatch;

public class usp_BADispatchReport_ByLocation_Result
{
    public int EmployerID { get; set; }
    public string Employer { get; set; } = string.Empty;
    public DateTime ReportAtTime { get; set; }
    public int RequestID { get; set; }
    public string ReportToName { get; set; } = string.Empty;
    public string ReportToPhone { get; set; } = string.Empty;
    public string BA { get; set; } = string.Empty;
    public string Requestor { get; set; } = string.Empty;
    public int? NWStewardID { get; set; }
    public int? HStewardID { get; set; }
    public string Show { get; set; } = string.Empty;
    public string Location { get; set; } = "Not Assigned";
    public string LocationSub { get; set; } = "Not Assigned";
    public string Booth { get; set; } = string.Empty;
}