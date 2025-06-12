using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Contracts.Reports.EmpDispatch;

public interface IEmpDispatchDocService
{
    Task GenerateEmpDispatchReportDocAsync(DispatchFilter filter);
}