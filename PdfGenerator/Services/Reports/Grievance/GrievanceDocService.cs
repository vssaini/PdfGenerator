using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Reports.Grievance
{
    public class GrievanceDocService(IGrievanceDocDataSource grvDocDs, ILogService logService) : IGrievanceDocService
    {
        public async Task GenerateGrievanceStepOneDocAsync(GrievanceFilter filter)
        {
            logService.LogInformation("Generating grievance document for grievance id {GrievanceId}", filter.GrievanceId);

            var model = await grvDocDs.GetGrievanceStepOneModelAsync(filter);

            var document = new GrievanceStepOneDocument(model);
            //document.GeneratePdf(); // TODO: Return bytes in Web app

            await document.ShowInPreviewerAsync();
        }
    }
}