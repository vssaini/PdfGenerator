using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Grievance;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Models.Enums;
using PdfGenerator.Models.Grievance.LetterStepOne;
using PdfGenerator.Models.Royalty;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace PdfGenerator.Services;

public class PdfService
{
    private readonly ILogger<PdfService> _logger;
    private readonly IInvoiceDocService _invDocService;
    private readonly IRoyaltyDocService _royDocService;
    private readonly IGrievanceDocService _grvDocService;

    public PdfService(ILogger<PdfService> logger, IInvoiceDocService invDocService, IRoyaltyDocService royDocService, IGrievanceDocService grvDocService)
    {
        _logger = logger;
        _invDocService = invDocService;
        _royDocService = royDocService;
        _grvDocService = grvDocService;
    }

    public async Task Run(Document document)
    {
        SetQuestPdfLicense();
        SetAppCulture();

        await GenerateDocumentAsync(document);
    }

    private void SetQuestPdfLicense()
    {
        _logger.LogInformation("Setting QuestPDF license");

        // Ref - https://www.questpdf.com/license/configuration.html
        QuestPDF.Settings.License = LicenseType.Community;
    }

    private void SetAppCulture()
    {
        _logger.LogInformation("Setting app culture to en-US");

        const string culture = "en-US";
        var cultureInfo = CultureInfo.GetCultureInfo(culture);

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }

    private async Task GenerateDocumentAsync(Document document)
    {
        switch (document)
        {
            case Document.Invoice:
                await _invDocService.GenerateInvoiceDocAsync();
                break;

            case Document.Royalty:
                var royFilter = new RoyaltyFilter(1997, 153043);
                await _royDocService.GenerateRoyaltyDocAsync(royFilter);
                break;

            case Document.GrievanceStepOneLetter:
                var grvFilter = new GrievanceFilter(3450);
                await _grvDocService.GenerateGrievanceStepOneDocAsync(grvFilter);
                break;
        }
    }
}  