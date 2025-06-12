using MediatR;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;

namespace PdfGenerator.Queries;

public sealed class GetGrievanceStepOneQuery(GrievanceFilter filter) : IRequest<GrievanceLetterStepOneModel>
{
    public GrievanceFilter Filter { get; set; } = filter;
}