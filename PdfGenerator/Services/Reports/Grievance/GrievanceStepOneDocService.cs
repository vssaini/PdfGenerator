using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using PdfGenerator.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;

namespace PdfGenerator.Services.Reports.Grievance;

//public class GrievanceStepOneDocService : IGrievanceDocService
public class GrievanceStepOneDocService(
    ILogger<GrievanceStepOneDocService> logger,
    ISender sender,
    IConfiguration config)
{
    public async Task GenerateGrievanceStepOneDocAsync(GrievanceFilter filter)
    {
        logger.LogInformation("Generating Grievance document for {GrievanceId}", filter.GrievanceId);

        var model = await sender.Send(new GetGrievanceStepOneQuery(filter));
        var document = new GrievanceStepOneDocument(model);

        var showInPreviewer = config.GetValue<bool>("Pdf:ShowInPreviewer");
        if (showInPreviewer)
            await document.ShowInPreviewerAsync();
        else
            GeneratePdf(document, "Grievance_Letter_Step1_NEW.pdf");
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);
        document.GeneratePdf(filePath);

        logger.LogInformation("Opening PDF at {PdfFilePath}", filePath);
        Process.Start("explorer.exe", filePath);
    }
}