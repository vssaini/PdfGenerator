﻿using MediatR;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using PdfGenerator.Queries;

namespace PdfGenerator.Handlers;

internal sealed class GetGrievanceStepOneQueryHandler : IRequestHandler<GetGrievanceStepOneQuery, GrievanceLetterStepOneModel>
{
    private readonly IGrievanceDocDataSource _grvDocDs;

    public GetGrievanceStepOneQueryHandler(IGrievanceDocDataSource grvDocDs)
    {
        _grvDocDs = grvDocDs;
    }

    public async Task<GrievanceLetterStepOneModel> Handle(GetGrievanceStepOneQuery request, CancellationToken cancellationToken)
    {
        return await _grvDocDs.GetGrievanceStepOneModelAsync(request.Filter);
    }
}