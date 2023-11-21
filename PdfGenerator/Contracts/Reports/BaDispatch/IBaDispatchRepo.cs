using PdfGenerator.Models.Reports.BaDispatch;

namespace PdfGenerator.Contracts.Reports.BaDispatch
{
    public interface IBaDispatchRepo
    {
        Task<List<BaDispatchResponse>> GetBaDispatchResponsesAsync(BaDispatchFilter filter);
    }
}