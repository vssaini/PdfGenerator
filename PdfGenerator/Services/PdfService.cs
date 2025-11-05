using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Invoice;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Contracts.Reports.Membership;
using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Models.Enums;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using PdfGenerator.Models.Royalty;
using QuestPDF.Infrastructure;
using SerilogTimings;
using System.Globalization;

namespace PdfGenerator.Services;

public class PdfService(
    ILogger<PdfService> logger,
    IInvoiceDocService invDocService,
    IRoyaltyDocService royDocService,
    IGrievanceDocService grvDocService,
    IBaDispatchDocService baDocService,
    IActiveMemberDocService amDocService,
    IDispatchWorkerListDocService dwlDocService,
    IDispatchSumDocService dsDocService,
    IEmpDispatchDocService empDocService)
{
    public async Task Run(Document document)
    {
        SetQuestPdfLicense();
        SetAppCulture();
        EnableQuestPdfDebugging(true);

        using var op = Operation.Begin("Generating report for document {Document}", document);
        await GenerateDocumentAsync(document);
        op.Complete();
    }

    private void SetQuestPdfLicense()
    {
        logger.LogInformation("Setting QuestPDF license");

        // Ref - https://www.questpdf.com/license/configuration.html
        QuestPDF.Settings.License = LicenseType.Community;
    }

    private void SetAppCulture()
    {
        logger.LogInformation("Setting app culture to en-US");

        const string culture = "en-US";
        var cultureInfo = CultureInfo.GetCultureInfo(culture);

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }

    private void EnableQuestPdfDebugging(bool enableDebugging)
    {
        if (enableDebugging)
            logger.LogInformation("Enabling debugging for QuestPDF");

        // To investigate the location of the root cause, please run the application with a debugger attached
        QuestPDF.Settings.EnableDebugging = enableDebugging;
    }

    private async Task GenerateDocumentAsync(Document document)
    {
        switch (document)
        {
            case Document.Invoice:
                await invDocService.GenerateInvoiceDocAsync();
                break;

            case Document.Royalty:
                var royFilter = new RoyaltyFilter(1997, 153043);
                await royDocService.GenerateRoyaltyDocAsync(royFilter);
                break;

            case Document.GrievanceStepOneLetter:
                var grvFilter = new GrievanceFilter(4140);
                await grvDocService.GenerateGrievanceStepOneDocAsync(grvFilter);
                break;

            case Document.BaDispatch:
                var startDate = new DateTime(2025, 01, 01);
                var endDate = new DateTime(2025, 02, 27);
                var dispatchFilter = new DispatchFilter(startDate, endDate, showPdfPreview: false);
                await baDocService.GenerateBaDispatchReportDocAsync(dispatchFilter);
                break;

            case Document.ActiveMember:
                await amDocService.GenerateActiveMemberDocAsync();
                break;

            case Document.RequestDispatchWorkerList:
                await dwlDocService.GenerateDispatchWorkerListDocAsync(317847);
                break;

            case Document.EBoardDispatchSummary:
                startDate = new DateTime(2024, 07, 12);
                endDate = new DateTime(2024, 07, 19);
                dispatchFilter = new DispatchFilter(startDate, endDate, showPdfPreview: false);
                await dsDocService.GenerateDispatchSummaryDocAsync(dispatchFilter);
                break;

            case Document.EmployerDispatch:
                startDate = new DateTime(2024, 07, 01);
                endDate = new DateTime(2024, 07, 10);
                dispatchFilter = new DispatchFilter(startDate, endDate, isPreview: true);
                await empDocService.GenerateEmpDispatchReportDocAsync(dispatchFilter);
                break;

            default:
                Console.WriteLine("Invalid document type");
                break;
        }
    }
}