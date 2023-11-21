using PdfGenerator.Models.Reports.BaDispatch;

namespace PdfGenerator.Contracts.Reports.BaDispatch
{
    public interface IBaDispatchDocDataSource
    {
        Task<BaDispatchReportModel> GetBaDispatchReportModelAsync(BaDispatchFilter filter);
    }
}