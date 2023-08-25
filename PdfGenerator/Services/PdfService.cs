using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace PdfGenerator.Services;

public class PdfService
{
    public static void SetQuestPdfLicense()
    {
        // Ref - https://www.questpdf.com/license/configuration.html
        QuestPDF.Settings.License = LicenseType.Community;

    }

    public static void GeneratePdf(IDocument document, string filePath)
    {
        document.GeneratePdf(filePath);
        Process.Start("explorer.exe", filePath);
    }
}