using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Properties;
using Footer = PdfGenerator.Models.Reports.BaDispatch.Footer;
using Header = PdfGenerator.Models.Reports.BaDispatch.Header;

namespace PdfGenerator.Data.Reports.BaDispatch
{
    public sealed class BaDispatchDocDataSource : IBaDispatchDocDataSource
    {
        private readonly IBaDispatchRepo _baRepo;

        public BaDispatchDocDataSource(IBaDispatchRepo baRepo)
        {
            _baRepo = baRepo;
        }

        public async Task<BaDispatchReportModel> GetBaDispatchReportModelAsync(BaDispatchFilter filter)
        {
            var baDispatchResponses = await _baRepo.GetBaDispatchResponsesAsync(filter);

            var header = new Header
            {
                Title = Resources.BaDispatch_Title,
                DateRange = $"Between {filter.StartDate:MM/dd/yyyy} AND {filter.EndDate:MM/dd/yyyy}"
            };

            var footer = new Footer
            {
                CurrentUserName = "Michael Stancliff",
                PropertyMessage = Resources.BaDispatch_PropertyMsg,
                CurrentDateTime = DateTime.Now.ToString("dddd, MMM dd, yyyy hh:mm tt")
            };

            return new BaDispatchReportModel
            {
                Header = header,
                Footer = footer,
                BaDispatchResponses = baDispatchResponses
            };
        }
    }
}
