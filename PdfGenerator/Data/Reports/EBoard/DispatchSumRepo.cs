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

    public async Task<usp_EBoard_DispatchSummary_Result> GetDispatchSummaryAsync(DispatchSumFilter filter)
    {
        var dParams = GetParamsForSp(filter);
        var disSumResp = await GetDispatchSummaryAsync(dParams);

        return disSumResp;
    }

    private static DynamicParameters GetParamsForSp(DispatchSumFilter filter)
    {
        var dParams = new DynamicParameters();
        dParams.Add("@StartDate", filter.StartDate);
        dParams.Add("@EndDate", filter.EndDate);

        return dParams;
    }

    private async Task<usp_EBoard_DispatchSummary_Result> GetDispatchSummaryAsync(SqlMapper.IDynamicParameters dParams)
    {
        const string spName = "dbo.usp_EBoard_DispatchSummary";
        _logger.LogInformation("Executing SP {SpName} to get dispatch summary data.", spName);

        var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

        using var connection = _sqlConnectionFactory.CreateConnection();

        await using var gr = await connection.QueryMultipleAsync(command);
        var disSumResp = gr.Read<usp_EBoard_DispatchSummary_Result>().FirstOrDefault();

        return disSumResp;
    }
}