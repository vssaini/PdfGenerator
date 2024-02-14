using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EBoard;
using PdfGenerator.Properties;

namespace PdfGenerator.Data.Reports.EBoard;

public class DispatchSumDocDataSource : IDispatchSumDocDataSource
{
    private readonly IDispatchSumRepo _dsRepo;
    private readonly ILogger<DispatchSumDocDataSource> _logger;
    private List<DispatchSumResponse> _disSumResponses;

    public DispatchSumDocDataSource(IDispatchSumRepo dsRepo, ILogger<DispatchSumDocDataSource> logger)
    {
        _dsRepo = dsRepo;
        _logger = logger;
    }

    public async Task<DispatchSumModel> GetDispatchSummaryModelAsync(DispatchSumFilter filter)
    {
        _disSumResponses = await _dsRepo.GetDispatchSummaryResponsesAsync(filter);

        _logger.LogInformation("Generating dispatch summary model for date range {StartDate} - {EndDate}", filter.StartDate, filter.EndDate);

        var header = new Header
        {
            Title = Resources.EbDispatchTitle,
            DateRange = $"Between {filter.StartDate:MM/dd/yyyy} AND {filter.EndDate:MM/dd/yyyy}"
        };

        var footer = new Footer
        {
            CurrentUserName = "Michael Stancliff",
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