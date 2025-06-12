using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Models.Reports.BaDispatch;

public class BaDispatchReportModel
{
    public Header Header { get; set; }
    public Footer Footer { get; set; }
    public List<BaDispatchResponse> BaDispatchResponses { get; set; }
}