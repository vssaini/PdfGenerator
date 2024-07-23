using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Models;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EmpDispatch;
using PdfGenerator.Properties;
using Footer = PdfGenerator.Models.Reports.Common.Footer;
using Header = PdfGenerator.Models.Reports.Common.Header;

namespace PdfGenerator.Data.Reports.EmpDispatch
{
    public sealed class EmpDispatchDocDataSource : IEmpDispatchDocDataSource
    {
        private readonly IEmpDispatchRepo _empRepo;
        private readonly ILogService _logService;

        public EmpDispatchDocDataSource(IEmpDispatchRepo empRepo, ILogService logService)
        {
            _empRepo = empRepo;
            _logService = logService;
        }

        public async Task<EmpDispatchReportModel> GetEmpDispatchReportModelAsync(DispatchFilter filter)
        {
            var empDispatchResponses = await _empRepo.GetEmpDispatchResponsesAsync(filter);

            _logService.LogInformation("Generating Employer Dispatch report model");

            var header = new Header
            {
                Title = Resources.EmpDispatchTitle,
                DateRange = $"Between {filter.StartDate:MM/dd/yyyy} AND {filter.EndDate:MM/dd/yyyy}"
            };

            var footer = new Footer
            {
                CurrentUserName = Constants.Username,
                PropertyMessage = Resources.PropertyMsg,
                CurrentDateTime = DateTime.Now.ToString("dddd, MMM dd, yyyy hh:mm tt")
            };

            return new EmpDispatchReportModel
            {
                Header = header,
                Footer = footer,
                EmpDispatchResponses = empDispatchResponses
            };
        }
    }
}
