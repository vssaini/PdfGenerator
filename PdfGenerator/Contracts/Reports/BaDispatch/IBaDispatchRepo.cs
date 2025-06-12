using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Contracts.Reports.BaDispatch;

public interface IBaDispatchRepo
{
    Task<List<BaDispatchResponse>> GetBaDispatchResponsesAsync(DispatchFilter filter);
}