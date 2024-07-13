using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Services.Helpers;

namespace PdfGenerator.Services.Reports.EmpDispatch
{
    public class EmpDispatchDocService : IEmpDispatchDocService
    {
        private readonly ILogService _logService;
        private readonly IEmpDispatchDocDataSource _empDocDs;

        public EmpDispatchDocService(IEmpDispatchDocDataSource empDocDs, ILogService logService)
        {
            _empDocDs = empDocDs;
            _logService = logService;
        }

        public async Task GenerateEmpDispatchReportDocAsync(DispatchFilter filter)
        {
            _logService.LogInformation("Generating Employer Dispatch report document");

            var model = await _empDocDs.GetEmpDispatchReportModelAsync(filter);
            var document = new EmpDispatchReportDocument(model);

            await PdfInvoker.ShowOrPreviewPdfAsync(filter, _logService, document);
        }
    }
}