using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EmpDispatch;

namespace PdfGenerator.Contracts.Reports.EmpDispatch;

public interface IEmpDispatchDocDataSource
{
    Task<EmpDispatchReportModel> GetEmpDispatchReportModelAsync(DispatchFilter filter);
}