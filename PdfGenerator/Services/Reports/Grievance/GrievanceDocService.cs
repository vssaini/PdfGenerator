using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using QuestPDF.Previewer;

namespace PdfGenerator.Services.Reports.Grievance
{
    public class GrievanceDocService : IGrievanceDocService
    {
        private readonly ILogService _logService;
        private readonly IGrievanceDocDataSource _grvDocDs;

        public GrievanceDocService(IGrievanceDocDataSource grvDocDs, ILogService logService)
        {
            _grvDocDs = grvDocDs;
            _logService = logService;
        }

        public async Task GenerateGrievanceStepOneDocAsync(GrievanceFilter filter)
        {
            _logService.LogInformation("Generating grievance document for grievance id {GrievanceId}", filter.GrievanceId);

            var model = await _grvDocDs.GetGrievanceStepOneModelAsync(filter);

            var document = new GrievanceStepOneDocument(model);
            //document.GeneratePdf(); // TODO: Return bytes in Web app

            await document.ShowInPreviewerAsync();
        }
    }
}