using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Services.Helpers;

namespace PdfGenerator.Services.Reports.EBoard;

public class DispatchSumDocService(IDispatchSumDocDataSource dsDocDs, ILogService logService) : IDispatchSumDocService
{
    public async Task GenerateDispatchSummaryDocAsync(DispatchFilter filter)
    {
        logService.LogInformation("Generating dispatch summary document");

        var model = await dsDocDs.GetDispatchSummaryModelAsync(filter);
        var document = new DispatchSumDocument(model);

        await PdfInvoker.HandlePdfDisplayAsync(filter, logService, document);
    }
}