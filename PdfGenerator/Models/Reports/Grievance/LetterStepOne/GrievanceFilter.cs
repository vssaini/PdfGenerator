using PdfGenerator.Models.Reports.Common;

namespace PdfGenerator.Models.Reports.Grievance.LetterStepOne;

public class GrievanceFilter : PdfFilter
{
    public int GrievanceId { get; set; }
        
    public GrievanceFilter(int grievanceId, bool showPdfPreview = true)
    {
        GrievanceId = grievanceId;
        ShowPdfPreview = showPdfPreview;
    }
}