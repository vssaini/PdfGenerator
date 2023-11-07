using PdfGenerator.Models.Grievance.LetterStepOne;
using PdfGenerator.Queries;

namespace PdfGenerator.Contracts.Grievance;

public interface IGrievanceDocDataSource
{
    Task<GrievanceLetterStepOneModel> GetGrievanceStepOneModelAsync(GetGrievanceStepOneQuery query);
}