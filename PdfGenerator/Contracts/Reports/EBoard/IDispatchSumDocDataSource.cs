using PdfGenerator.Models.Reports.EBoard;

namespace PdfGenerator.Contracts.Reports.EBoard;

public interface IDispatchSumDocDataSource
{
    Task<DispatchSumModel> GetDispatchSummaryModelAsync(DispatchSumFilter filter);
}