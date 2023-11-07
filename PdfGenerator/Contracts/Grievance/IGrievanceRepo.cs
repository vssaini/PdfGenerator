using PdfGenerator.Models.Grievance.LetterStepOne;
using PdfGenerator.Queries;

namespace PdfGenerator.Contracts.Grievance;

public interface IGrievanceRepo
{
    Task<GrievanceLetterStepOneResponse> GetGrievanceLetterStepOneAsync(GetGrievanceStepOneQuery query);
}