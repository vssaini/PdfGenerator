using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models;
using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Properties;
using Footer = PdfGenerator.Models.Reports.Common.Footer;
using Header = PdfGenerator.Models.Reports.Common.Header;

namespace PdfGenerator.Data.Reports.BaDispatch;

public sealed class BaDispatchDocDataSource(IBaDispatchRepo baRepo, ILogService logService)
    : IBaDispatchDocDataSource
{
    public async Task<BaDispatchReportModel> GetBaDispatchReportModelAsync(DispatchFilter filter)
    {
        var baDispatchResponses = await baRepo.GetBaDispatchResponsesAsync(filter);

        logService.LogInformation("Generating BA Dispatch report model");

        var header = new Header
        {
            Title = Resources.BaDispatchTitle,
            DateRange = $"Between {filter.StartDate:MM/dd/yyyy} AND {filter.EndDate:MM/dd/yyyy}"
        };

        var footer = new Footer
        {
            CurrentUserName = Constants.Username,
            PropertyMessage = Resources.PropertyMsg,
            CurrentDateTime = DateTime.Now.ToString("dddd, MMM dd, yyyy hh:mm tt")
        };

        return new BaDispatchReportModel
        {
            Header = header,
            Footer = footer,
            BaDispatchResponses = baDispatchResponses
        };
    }
}