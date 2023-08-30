using QuestPDF.Infrastructure;

namespace PdfGenerator.Contracts;

public interface IDocService
{
    void GenerateDoc(bool showInPreviewer = false, int fontSize = 8);
    void GeneratePdf(IDocument document, string filePath);
}