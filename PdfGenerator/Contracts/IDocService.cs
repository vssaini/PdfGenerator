namespace PdfGenerator.Contracts;

public interface IDocService
{
    void GenerateDoc(bool showInPreviewer, int fontSize);
}