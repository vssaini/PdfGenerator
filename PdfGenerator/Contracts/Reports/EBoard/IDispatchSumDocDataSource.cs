using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EBoard;

namespace PdfGenerator.Contracts.Reports.EBoard;

public interface IDispatchSumDocDataSource
{
    Task<DispatchSumModel> GetDispatchSummaryModelAsync(DispatchFilter filter);
}