using PdfGenerator.Models.Reports.Request;

namespace PdfGenerator.Contracts.Reports.Request;

public interface IDispatchWorkerListDocDataSource
{
    Task<DispatchWorkerListReportModel> GetDispatchWorkerListModelAsync(int requestId);
}