using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Services.Helpers;

namespace PdfGenerator.Services.Reports.EmpDispatch
{
    public class EmpDispatchDocService(IEmpDispatchDocDataSource empDocDs, ILogService logService)
        : IEmpDispatchDocService
    {
        public async Task GenerateEmpDispatchReportDocAsync(DispatchFilter filter)
        {
            logService.LogInformation("Generating Employer Dispatch report document");

            var model = await empDocDs.GetEmpDispatchReportModelAsync(filter);
            var document = new EmpDispatchReportDocument(model);

            await PdfInvoker.ShowOrPreviewPdfAsync(filter, logService, document);
        }
    }
}