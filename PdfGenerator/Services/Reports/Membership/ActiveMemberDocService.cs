using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Membership;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Reports.Membership
{
    public class ActiveMemberDocService : IActiveMemberDocService
    {
        private readonly ILogService _logService;
        private readonly IActiveMemberDocDataSource _amDocDs;

        public ActiveMemberDocService(IActiveMemberDocDataSource amDocDs, ILogService logService)
        {
            _amDocDs = amDocDs;
            _logService = logService;
        }

        public async Task GenerateActiveMemberDocAsync()
        {
            _logService.LogInformation("Generating Active Members report document");

            var model = await _amDocDs.GetActiveMemberModelAsync();

            var document = new ActiveMemberDocument(model);

            await document.ShowInPreviewerAsync();
        }
    }
}