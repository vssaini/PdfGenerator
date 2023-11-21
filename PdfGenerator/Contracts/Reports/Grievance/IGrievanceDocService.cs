using PdfGenerator.Models.Reports.Grievance.LetterStepOne;

namespace PdfGenerator.Contracts.Reports.Grievance
{
    public interface IGrievanceDocService
    {
        Task GenerateGrievanceStepOneDocAsync(GrievanceFilter filter);
    }
}