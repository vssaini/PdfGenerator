using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Models;
using PdfGenerator.Models.Reports.Request;
using PdfGenerator.Properties;

namespace PdfGenerator.Data.Reports.Request;

public sealed class DispatchWorkerListDocDataSource(IDispatchWorkerListRepo dwlRepo, ILogService logService)
    : IDispatchWorkerListDocDataSource
{
    public async Task<DispatchWorkerListReportModel> GetDispatchWorkerListModelAsync(int requestId)
    {
        logService.LogInformation($"Generating Dispatch Worker List report model");

        var header = new Header
        {
            Title = "Dispatch Worker List"
        };

        var footer = new Footer
        {
            CurrentUserName = Constants.Username,
            PropertyMessage = Resources.PropertyMsg,
            CurrentDateTime = DateTime.Now.ToString("dddd, MMM dd, yyyy hh:mm tt")
        };

        var wlReportVm = await dwlRepo.GetDispatchWorkerAsync(requestId);

        return new DispatchWorkerListReportModel
        {
            Header = header,
            Footer = footer,
            DispatchSummary = wlReportVm.RequestHeader,
            Workers = wlReportVm.Workers
        };
    }
}