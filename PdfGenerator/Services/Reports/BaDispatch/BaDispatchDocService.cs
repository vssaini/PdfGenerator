using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Reports.BaDispatch
{
    public class BaDispatchDocService : IBaDispatchDocService
    {
        private readonly ILogService _logService;
        private readonly IBaDispatchDocDataSource _baDocDs;

        public BaDispatchDocService(IBaDispatchDocDataSource baDocDs, ILogService logService)
        {
            _baDocDs = baDocDs;
            _logService = logService;
        }

        public async Task GenerateBaDispatchReportDocAsync(BaDispatchFilter filter)
        {
            _logService.LogInformation("Generating BA Dispatch report document");

            var model = await _baDocDs.GetBaDispatchReportModelAsync(filter);

            var document = new BaDispatchReportDocument(model);
            //return document.GeneratePdf(); // TODO: Return bytes in Web app

            await document.ShowInPreviewerAsync();
        }
    }
}