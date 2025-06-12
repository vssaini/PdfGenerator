using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Membership;
using QuestPDF.Companion;

namespace PdfGenerator.Services.Reports.Membership;

public class ActiveMemberDocService(IActiveMemberDocDataSource amDocDs, ILogService logService)
    : IActiveMemberDocService
{
    public async Task GenerateActiveMemberDocAsync()
    {
        logService.LogInformation("Generating Active Members report document");

        var model = await amDocDs.GetActiveMemberModelAsync();

        var document = new ActiveMemberDocument(model);

        await document.ShowInCompanionAsync();
    }
}