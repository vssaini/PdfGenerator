using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace PdfGenerator.Services;

public class PdfService : IPdfService
{
    private readonly ILogger<PdfService> _logger;
    private readonly IDocService _docService;

    public PdfService(ILogger<PdfService> logger, IDocService docService)
    {
        _logger = logger;
        _docService = docService;
    }

    public void Run()
    {
        SetQuestPdfLicense();
        SetAppCulture();

        _docService.GenerateDoc();
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
}