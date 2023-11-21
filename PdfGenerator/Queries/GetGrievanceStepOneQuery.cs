using MediatR;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;

namespace PdfGenerator.Queries;

public sealed class GetGrievanceStepOneQuery : IRequest<GrievanceLetterStepOneModel>
{
    public GrievanceFilter Filter { get; set; }

    public GetGrievanceStepOneQuery(GrievanceFilter filter)
    {
        Filter = filter;
    }
}