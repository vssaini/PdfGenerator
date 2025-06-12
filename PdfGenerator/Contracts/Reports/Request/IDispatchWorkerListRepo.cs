using PdfGenerator.Models.Reports.Request;

namespace PdfGenerator.Contracts.Reports.Request;

public interface IDispatchWorkerListRepo
{
    Task<RequestWorkerListReportVm> GetDispatchWorkerAsync(int requestId);
}