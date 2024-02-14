using PdfGenerator.Models.Reports.EBoard;

namespace PdfGenerator.Contracts.Reports.EBoard;

public interface IDispatchSumRepo
{
    Task<List<DispatchSumResponse>> GetDispatchSummaryResponsesAsync(DispatchSumFilter filter);
}