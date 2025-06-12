using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Request;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Reports.Request
{
    public class DispatchWorkerListDocService(IDispatchWorkerListDocDataSource dwlDocDs, ILogService logService)
        : IDispatchWorkerListDocService
    {
        public async Task GenerateDispatchWorkerListDocAsync(int requestId)
        {
            logService.LogInformation("Generating Dispatch Worker List report document");

            var model = await dwlDocDs.GetDispatchWorkerListModelAsync(requestId);

            var document = new DispatchWorkerListDocument(model);

            await document.ShowInPreviewerAsync();
        }
    }
}