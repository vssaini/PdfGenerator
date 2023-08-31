using QuestPDF.Infrastructure;

namespace PdfGenerator.Contracts;

public interface IDocService
{
    Task GenerateDocAsync();
    void GeneratePdf(IDocument document, string filePath);
}