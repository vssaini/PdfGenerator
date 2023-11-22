namespace PdfGenerator.Models.Reports.BaDispatch;

public partial class usp_BADispatchReport_ByLocation_Result
{
    public int EmployerID { get; set; }
    public string Employer { get; set; }
    public string Location { get; set; }
    public int RequestID { get; set; }
    public string ReportToName { get; set; }
    public string ReportToPhone { get; set; }
    public string BA { get; set; }
    public string Requestor { get; set; }
    public int? NWStewardID { get; set; }
    public int? HStewardID { get; set; }
    public string Show { get; set; }
}