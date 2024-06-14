using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EmpDispatch;

namespace PdfGenerator.Contracts.Reports.EmpDispatch
{
    public interface IEmpDispatchRepo
    {
        Task<List<EmpDispatchResponse>> GetEmpDispatchResponsesAsync(DispatchFilter filter);
    }
}