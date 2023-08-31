using PdfGenerator.Models;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Contracts;

public interface IDocService
{
    Task GenerateDocAsync(DocFilter filter);
    void GeneratePdf(IDocument document, string filePath);
}