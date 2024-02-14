using Dapper;
using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models.Reports.EBoard;
using System.Data;

namespace PdfGenerator.Data.Reports.EBoard;

public class DispatchSumRepo : IDispatchSumRepo
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ILogger<DispatchSumRepo> _logger;

    public DispatchSumRepo(ISqlConnectionFactory sqlConnectionFactory, ILogger<DispatchSumRepo> logger)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _logger = logger;
    }

    public async Task<List<DispatchSumResponse>> GetDispatchSummaryResponsesAsync(DispatchSumFilter filter)
    {
        var dParams = GetParamsForSp(filter);
        var disSummaries = await GetDispatchSummariesAsync(dParams);

        var disSumResponses = disSummaries
            .OrderBy(x => x.ReportAtTime)
            .GroupBy(r => r.ReportAtTime)
            .Select(g => new DispatchSumResponse
            {
                Date = g.Key ?? DateTime.MinValue,
                DispatchSumEmpRows = GetDispatchSummaryEmployerRows(g)
            })
            .ToList();

        return disSumResponses;
    }

    private static DynamicParameters GetParamsForSp(DispatchSumFilter filter)
    {
        var dParams = new DynamicParameters();
        dParams.Add("@StartDate", filter.StartDate);
        dParams.Add("@EndDate", filter.EndDate);

        return dParams;
    }

    private async Task<List<usp_EBoard_DispatchSummary_Result>> GetDispatchSummariesAsync(SqlMapper.IDynamicParameters dParams)
    {
        const string spName = "dbo.usp_EBoard_DispatchSummary";
        _logger.LogInformation("Executing SP {SpName} to get dispatch summary data.", spName);

        var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

        using var connection = _sqlConnectionFactory.CreateConnection();

        await using var gr = await connection.QueryMultipleAsync(command);
        var disSumResults = gr.Read<usp_EBoard_DispatchSummary_Result>();

        return disSumResults.ToList();
    }

    private static List<DispatchSumEmpRow> GetDispatchSummaryEmployerRows(IEnumerable<usp_EBoard_DispatchSummary_Result> grp)
    {
        return grp
            .OrderBy(x => x.Employer)
            .GroupBy(x => x.Employer)
            .Select(g => new DispatchSumEmpRow
            {
                Employer = g.Key,
                SummaryRows = GetDispatchSummaryRows(g)
            })
            .ToList();
    }

    private static List<DispatchSumRow> GetDispatchSummaryRows(IEnumerable<usp_EBoard_DispatchSummary_Result> grp)
    {
        return grp
            .Select(x => new DispatchSumRow
            {
                Id = x.RequestID,
                Facility = x.Location,
                Location = x.LocationSub,
                ShowName = x.ShowName,
                DispatchCount = x.Dispatches ?? 0
            })
            .ToList();
    }
}