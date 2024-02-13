using PdfGenerator.Models.Reports.EBoard;

namespace PdfGenerator.Contracts.Reports.EBoard;

public interface IDispatchSumDocService
{
    Task GenerateDispatchSummaryDocAsync(DispatchSumFilter filter);
}