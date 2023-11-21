using PdfGenerator.Models.Reports.Grievance.LetterStepOne;

namespace PdfGenerator.Contracts.Reports.Grievance
{
    public interface IGrievanceRepo
    {
        Task<GrievanceLetterStepOneResponse> GetGrievanceLetterStepOneAsync(GrievanceFilter filter);
    }
}