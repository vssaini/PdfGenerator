using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Contracts.Reports.Membership;
using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Models.Enums;
using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Models.Reports.EBoard;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
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
    private readonly IBaDispatchDocService _baDocService;
    private readonly IActiveMemberDocService _amDocService;
    private readonly IDispatchWorkerListDocService _dwlDocService;
    private readonly IDispatchSumDocService _dsDocService;

    public PdfService(ILogger<PdfService> logger, IInvoiceDocService invDocService, IRoyaltyDocService royDocService, IGrievanceDocService grvDocService, IBaDispatchDocService baDocService, IActiveMemberDocService amDocService, IDispatchWorkerListDocService dwlDocService, IDispatchSumDocService dsDocService)
    {
        _logger = logger;
        _invDocService = invDocService;
        _royDocService = royDocService;
        _grvDocService = grvDocService;
        _baDocService = baDocService;
        _amDocService = amDocService;
        _dwlDocService = dwlDocService;
        _dsDocService = dsDocService;
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
                var grvFilter = new GrievanceFilter(4140);
                await _grvDocService.GenerateGrievanceStepOneDocAsync(grvFilter);
                break;

            case Document.BaDispatch:
                var startDate = new DateTime(2023, 10, 22);
                var endDate = DateTime.Now;
                var baDispatchFilter = new BaDispatchFilter(startDate, endDate);
                await _baDocService.GenerateBaDispatchReportDocAsync(baDispatchFilter);
                break;

            case Document.ActiveMember:
                await _amDocService.GenerateActiveMemberDocAsync();
                break;

            case Document.RequestDispatchWorkerList:
                await _dwlDocService.GenerateDispatchWorkerListDocAsync(279288);
                break;

            case Document.EBoardDispatchSummary:
                startDate = new DateTime(2024, 02, 05);
                var disSumFilter = new DispatchSumFilter(startDate, startDate);
                await _dsDocService.GenerateDispatchSummaryDocAsync(disSumFilter);
                break;

            default:
                Console.WriteLine("Invalid document type");
                break;
        }
    }
}