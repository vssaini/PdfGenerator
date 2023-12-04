using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Models.Reports.Request;

namespace PdfGenerator.Data.Reports.Request
{
    public sealed class DispatchWorkerListDocDataSource : IDispatchWorkerListDocDataSource
    {
        private readonly IDispatchWorkerListRepo _dwlRepo;

        public DispatchWorkerListDocDataSource(IDispatchWorkerListRepo dwlRepo)
        {
            _dwlRepo = dwlRepo;
        }

        public Task<RequestWorkerListReportVm> GetDispatchWorkerListModelAsync(int requestId)
        {
            return _dwlRepo.GetDispatchWorkerAsync(requestId);
        }
    }
}
