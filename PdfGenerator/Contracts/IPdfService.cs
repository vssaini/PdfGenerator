using QuestPDF.Infrastructure;

namespace PdfGenerator.Contracts;

public interface IPdfService
{
    void GeneratePdf(IDocument document, string filePath);

}