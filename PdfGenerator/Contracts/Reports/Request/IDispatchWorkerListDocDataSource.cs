using PdfGenerator.Models.Reports.Request;

namespace PdfGenerator.Contracts.Reports.Request
{
    public interface IDispatchWorkerListDocDataSource
    {
        Task<RequestWorkerListReportVm> GetDispatchWorkerListModelAsync(int requestId);
    }
}