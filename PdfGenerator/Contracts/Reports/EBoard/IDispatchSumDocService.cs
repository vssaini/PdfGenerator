using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Contracts.Reports.EBoard;

public interface IDispatchSumDocService
{
    Task GenerateDispatchSummaryDocAsync(DispatchFilter filter);
}