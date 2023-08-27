using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.Globalization;

namespace PdfGenerator.Services;

public class PdfService
{
    public static void SetQuestPdfLicense()
    {
        // Ref - https://www.questpdf.com/license/configuration.html
        QuestPDF.Settings.License = LicenseType.Community;

    }

    public static void SetAppCulture()
    {
        const string culture = "en-US";
        var cultureInfo = CultureInfo.GetCultureInfo(culture);

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }

    public static void GeneratePdf(IDocument document, string filePath)
    {
        document.GeneratePdf(filePath);
        Process.Start("explorer.exe", filePath);
    }
}