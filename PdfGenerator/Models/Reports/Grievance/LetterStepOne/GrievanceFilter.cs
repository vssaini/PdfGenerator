namespace PdfGenerator.Models.Reports.Grievance.LetterStepOne
{
    public class GrievanceFilter
    {
        public int GrievanceId { get; set; }

        public GrievanceFilter(int grievanceId)
        {
            GrievanceId = grievanceId;
        }
    }
}