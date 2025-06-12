using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Contracts.Reports.BaDispatch;

public interface IBaDispatchDocDataSource
{
    Task<BaDispatchReportModel> GetBaDispatchReportModelAsync(DispatchFilter filter);
}