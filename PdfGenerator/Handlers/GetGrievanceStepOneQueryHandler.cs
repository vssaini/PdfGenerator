using MediatR;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using PdfGenerator.Queries;

namespace PdfGenerator.Handlers;

internal sealed class GetGrievanceStepOneQueryHandler(IGrievanceDocDataSource grvDocDs)
    : IRequestHandler<GetGrievanceStepOneQuery, GrievanceLetterStepOneModel>
{
    public async Task<GrievanceLetterStepOneModel> Handle(GetGrievanceStepOneQuery request, CancellationToken cancellationToken)
    {
        return await grvDocDs.GetGrievanceStepOneModelAsync(request.Filter);
    }
}