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
public class GrievanceStepOneDocService
{
    private readonly ILogger<GrievanceStepOneDocService> _logger;
    private readonly ISender _sender;
    private readonly IConfiguration _config;

    public GrievanceStepOneDocService(ILogger<GrievanceStepOneDocService> logger, ISender sender, IConfiguration config)
    {
        _logger = logger;
        _sender = sender;
        _config = config;
    }

    public async Task GenerateGrievanceStepOneDocAsync(GrievanceFilter filter)
    {
        _logger.LogInformation("Generating Grievance document for {GrievanceId}", filter.GrievanceId);

        var model = await _sender.Send(new GetGrievanceStepOneQuery(filter));
        var document = new GrievanceStepOneDocument(model);

        var showInPreviewer = _config.GetValue<bool>("Pdf:ShowInPreviewer");
        if (showInPreviewer)
            await document.ShowInPreviewerAsync();
        else
            GeneratePdf(document, "Grievance_Letter_Step1_NEW.pdf");
    }

    public void GeneratePdf(IDocument document, string filePath)
    {
        _logger.LogInformation("Generating PDF at {PdfFilePath}", filePath);
        document.GeneratePdf(filePath);

        _logger.LogInformation("Opening PDF at {PdfFilePath}", filePath);
        Process.Start("explorer.exe", filePath);
    }
}