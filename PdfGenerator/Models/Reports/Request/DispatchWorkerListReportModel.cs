using PdfGenerator.Models.Reports.BaDispatch;

namespace PdfGenerator.Models.Reports.Request
{
    public class DispatchWorkerListReportModel
    {
        public Header Header { get; set; }
        public Footer Footer { get; set; }
        public RequestWorkerListReportVm WorkerListModel { get; set; }
    }
}