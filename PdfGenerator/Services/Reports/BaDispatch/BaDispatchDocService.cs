using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Services.Helpers;

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

        public async Task GenerateBaDispatchReportDocAsync(DispatchFilter filter)
        {
            _logService.LogInformation("Generating BA Dispatch report document");

            var model = await _baDocDs.GetBaDispatchReportModelAsync(filter);
            var document = new BaDispatchReportDocument(model);

            await PdfInvoker.ShowOrPreviewPdfAsync(filter, _logService, document);
        }
    }
}