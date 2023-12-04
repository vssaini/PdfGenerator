using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Models.Reports.Request;
using PdfGenerator.Properties;

namespace PdfGenerator.Data.Reports.Request
{
    public sealed class DispatchWorkerListDocDataSource : IDispatchWorkerListDocDataSource
    {
        private readonly IDispatchWorkerListRepo _dwlRepo;
        private readonly ILogService _logService;

        public DispatchWorkerListDocDataSource(IDispatchWorkerListRepo dwlRepo, ILogService logService)
        {
            _dwlRepo = dwlRepo;
            _logService = logService;
        }

        public async Task<DispatchWorkerListReportModel> GetDispatchWorkerListModelAsync(int requestId)
        {
            _logService.LogInformation($"Generating Dispatch Worker List report model");

            var header = new Header
            {
                Title = "Dispatch Worker List"
            };

            var footer = new Footer
            {
                CurrentUserName = "Michael Stancliff",
                PropertyMessage = Resources.PropertyMsg,
                CurrentDateTime = DateTime.Now.ToString("dddd, MMM dd, yyyy hh:mm tt")
            };

            var workerListModel = await _dwlRepo.GetDispatchWorkerAsync(requestId);

            return new DispatchWorkerListReportModel
            {
                Header = header,
                Footer = footer,
                WorkerListModel = workerListModel
            };
        }
    }
}
