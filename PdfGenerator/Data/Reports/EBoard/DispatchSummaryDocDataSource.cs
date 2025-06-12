using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EBoard;
using PdfGenerator.Properties;

namespace PdfGenerator.Data.Reports.EBoard;

public class DispatchSumDocDataSource(IDispatchSumRepo dsRepo, ILogger<DispatchSumDocDataSource> logger)
    : IDispatchSumDocDataSource
{
    private List<DispatchSumResponse> _disSumResponses;

    public async Task<DispatchSumModel> GetDispatchSummaryModelAsync(DispatchFilter filter)
    {
        _disSumResponses = await dsRepo.GetDispatchSummaryResponsesAsync(filter);

        logger.LogInformation("Generating dispatch summary model for date range {StartDate} - {EndDate}", filter.StartDate, filter.EndDate);

        var header = new Header
        {
            Title = Resources.EbDispatchTitle,
            DateRange = $"Between {filter.StartDate:MM/dd/yyyy} AND {filter.EndDate:MM/dd/yyyy}"
        };

        var footer = new Footer
        {
            CurrentUserName = Constants.Username,
            PropertyMessage = Resources.PropertyMsg,
            CurrentDateTime = DateTime.Now.ToString("dddd, MMM dd, yyyy hh:mm tt")
        };

        return new DispatchSumModel
        {
            Header = header,
            Footer = footer,
            DispatchSumResponses = _disSumResponses
        };
    }
}