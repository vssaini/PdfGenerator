namespace PdfGenerator.Models.Reports.Common;

public abstract class PdfFilter
{
    /// <summary>
    /// Gets or sets to show PDF in QuestPDF previewer; otherwise generate PDF.
    /// </summary>
    public bool ShowPdfPreview { get; set; }

    /// <summary>
    /// Gets or sets if the request is for 'Preview' purpose only.
    /// </summary>
    public bool IsPreview { get; set; }
}