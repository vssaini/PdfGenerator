namespace PdfGenerator.Models.Reports.Request;

public class DispatchWorkerListReportModel
{
    public Header Header { get; set; }
    public Footer Footer { get; set; }

    public RequestHeaderVm DispatchSummary { get; set; }
    public List<RequestWorkerListVm> Workers { get; set; }
}