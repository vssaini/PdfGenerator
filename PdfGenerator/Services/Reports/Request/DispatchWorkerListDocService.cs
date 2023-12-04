using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Request;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Reports.Request
{
    public class DispatchWorkerListDocService : IDispatchWorkerListDocService
    {
        private readonly ILogService _logService;
        private readonly IDispatchWorkerListDocDataSource _dwlDocDs;

        public DispatchWorkerListDocService(IDispatchWorkerListDocDataSource dwlDocDs, ILogService logService)
        {
            _dwlDocDs = dwlDocDs;
            _logService = logService;
        }

        public async Task GenerateDispatchWorkerListDocAsync(int requestId)
        {
            _logService.LogInformation("Generating Dispatch Worker List report document");

            var model = await _dwlDocDs.GetDispatchWorkerListModelAsync(requestId);

            var document = new DispatchWorkerListDocument(model);

            await document.ShowInPreviewerAsync();
        }
    }
}