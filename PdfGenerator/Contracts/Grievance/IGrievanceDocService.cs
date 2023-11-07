using PdfGenerator.Models.Grievance.LetterStepOne;

namespace PdfGenerator.Contracts.Grievance;

public interface IGrievanceDocService
{
    Task GenerateGrievanceStepOneDocAsync(GrievanceFilter filter);
}