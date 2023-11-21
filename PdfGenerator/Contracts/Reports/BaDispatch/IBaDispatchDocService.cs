using PdfGenerator.Models.Reports.BaDispatch;

namespace PdfGenerator.Contracts.Reports.BaDispatch
{
    public interface IBaDispatchDocService
    {
        Task<byte[]> GenerateBaDispatchReportDocAsync(BaDispatchFilter filter);
    }
}