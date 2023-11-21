using PdfGenerator.Models.Reports.Grievance.LetterStepOne;

namespace PdfGenerator.Contracts.Reports.Grievance
{
    public interface IGrievanceDocDataSource
    {
        Task<GrievanceLetterStepOneModel> GetGrievanceStepOneModelAsync(GrievanceFilter filter);
    }
}