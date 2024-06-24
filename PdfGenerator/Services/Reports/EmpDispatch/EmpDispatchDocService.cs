using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Models.Reports.Common;
using QuestPDF.Fluent;
using QuestPDF.Previewer;

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

            //_logService.LogInformation("Generating and showing Employer Dispatch PDF");
            //document.GeneratePdfAndShow();

            _logService.LogInformation("Showing Employer Dispatch report PDF in Previewer");
            await document.ShowInPreviewerAsync();
        }
    }
}