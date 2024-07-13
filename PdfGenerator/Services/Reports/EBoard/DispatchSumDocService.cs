using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Services.Helpers;

namespace PdfGenerator.Services.Reports.EBoard;

public class DispatchSumDocService : IDispatchSumDocService
{
    private readonly IDispatchSumDocDataSource _dsDocDs;
    private readonly ILogService _logService;

    public DispatchSumDocService(IDispatchSumDocDataSource dsDocDs, ILogService logService)
    {
        _dsDocDs = dsDocDs;
        _logService = logService;
    }

    public async Task GenerateDispatchSummaryDocAsync(DispatchFilter filter)
    {
        _logService.LogInformation("Generating dispatch summary document");

        var model = await _dsDocDs.GetDispatchSummaryModelAsync(filter);
        var document = new DispatchSumDocument(model);

        await PdfInvoker.ShowOrPreviewPdfAsync(filter, _logService, document);
    }
}