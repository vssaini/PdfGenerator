using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Models.Reports.EBoard;

public class DispatchSumModel
{
    public Header Header { get; set; }
    public Footer Footer { get; set; }

    public List<DispatchSumResponse> DispatchSumResponses { get; set; }
}