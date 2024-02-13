using PdfGenerator.Models.Reports.EBoard;

namespace PdfGenerator.Contracts.Reports.EBoard;

public interface IDispatchSumRepo
{
    Task<usp_EBoard_DispatchSummary_Result> GetDispatchSummaryAsync(DispatchSumFilter filter);
}