using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Grievance;
using PdfGenerator.Models.Grievance.LetterStepOne;
using PdfGenerator.Queries;
using System.Data;

namespace PdfGenerator.Data.Grievance;

public class GrievanceRepo : IGrievanceRepo
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GrievanceRepo(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<GrievanceLetterStepOneResponse> GetGrievanceLetterStepOneAsync(GetGrievanceStepOneQuery query)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var dParams = GetParamsForSp(query);
        var grvLetStepOneResp = await GetGrievanceLetterStepOneAsync(dParams);

        return grvLetStepOneResp;
    }

    private static DynamicParameters GetParamsForSp(GetGrievanceStepOneQuery query)
    {
        var dParams = new DynamicParameters();
        dParams.Add("@GrievanceId", query.Filter.GrievanceId);

        return dParams;
    }

    private async Task<GrievanceLetterStepOneResponse> GetGrievanceLetterStepOneAsync(SqlMapper.IDynamicParameters dParams)
    {
        const string spName = "dbo.usp_Grievance_Letter_StepOne";
        var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

        using var connection = _sqlConnectionFactory.CreateConnection();

        await using var gr = await connection.QueryMultipleAsync(command);
        var grvLetStepOneResp = gr.Read<GrievanceLetterStepOneResponse>().FirstOrDefault();

        return grvLetStepOneResp;
    }
}