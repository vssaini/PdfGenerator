using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Services.Helpers;

namespace PdfGenerator.Services.Reports.BaDispatch;

public class BaDispatchDocService(IBaDispatchDocDataSource baDocDs, ILogService logService) : IBaDispatchDocService
{
    public async Task GenerateBaDispatchReportDocAsync(DispatchFilter filter)
    {
        logService.LogInformation("Generating BA Dispatch report document");

        var model = await baDocDs.GetBaDispatchReportModelAsync(filter);
        var document = new BaDispatchReportDocument(model);

        await PdfInvoker.ShowOrPreviewPdfAsync(filter, logService, document);
    }
}