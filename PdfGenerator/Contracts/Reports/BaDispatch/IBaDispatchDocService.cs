using PdfGenerator.Models.Reports.BaDispatch;

namespace PdfGenerator.Contracts.Reports.BaDispatch
{
    public interface IBaDispatchDocService
    {
        Task GenerateBaDispatchReportDocAsync(BaDispatchFilter filter);
    }
}