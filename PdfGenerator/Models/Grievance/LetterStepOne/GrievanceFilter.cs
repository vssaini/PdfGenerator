namespace PdfGenerator.Models.Grievance.LetterStepOne;

public class GrievanceFilter
{
    public int GrievanceId { get; set; }

    public GrievanceFilter(int grievanceId)
    {
        GrievanceId = grievanceId;
    }
}