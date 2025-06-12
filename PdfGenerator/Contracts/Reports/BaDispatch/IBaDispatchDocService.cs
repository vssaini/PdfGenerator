using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Contracts.Reports.BaDispatch;

public interface IBaDispatchDocService
{
    Task GenerateBaDispatchReportDocAsync(DispatchFilter filter);
}