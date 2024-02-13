using System.ComponentModel.DataAnnotations;

namespace PdfGenerator.Models.Reports.Request;

public class RequestWorkerListReportVm
{
    public int RequestID { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<RequestWorkerListVm> Workers { get; set; }
    public RequestHeaderVm RequestHeader { get; set; }
}

public class RequestWorkerListVm
{
    public int RequestID2 { get; set; }
    public int RequestDtlID { get; set; }
    public int DispatchID { get; set; }
    public string WorkerName { get; set; }
    public int WorkerID { get; set; }
    public string PhoneType { get; set; }
    [UIHint("Phone")]
    public string Number { get; set; }
    public string ReportTime { get; set; }
    public int Quantity { get; set; }
    public string OriginalSkil { get; set; }
    public string DispatchSkill { get; set; }
    public bool ExtendedHoursPay { get; set; }
    public string EmailPersonal { get; set; }
    public string NotForOpen { get; set; }
}

public class RequestHeaderVm
{
    public int RequestID { get; set; }
    public string Employer { get; set; }
    public DateTime? RequestReceived { get; set; }
    public string Coordinator { get; set; }
    public string ReportToName { get; set; }
    public string CallNo { get; set; }
    public DateTime? ReportAtTime { get; set; }
    public string Location { get; set; }
    public string LocationSub { get; set; }
    public string Booth { get; set; }
    public string Show { get; set; }
    public int? WorkersDispatched { get; set; }
    public int? WorkersRequested { get; set; }
    public string BusinessAgent { get; set; }
    public string ApprovalStatus { get; set; }
}