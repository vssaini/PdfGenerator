using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Previewer;

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

    public async Task GenerateDispatchSummaryDocAsync(DispatchSumFilter filter)
    {
        _logService.LogInformation("Generating dispatch summary document");

        var model = await _dsDocDs.GetDispatchSummaryModelAsync(filter);
        var document = new DispatchSumDocument(model);

        //document.GeneratePdf(); // TODO: Return bytes in Web app

        await document.ShowInPreviewerAsync();
    }
}