namespace PdfGenerator.Contracts;

public interface IDocService
{
    void GenerateDoc(bool showInPreviewer = false, int fontSize = 8);
}