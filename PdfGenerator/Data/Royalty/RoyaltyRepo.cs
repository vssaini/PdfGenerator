using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Royalty;
using PdfGenerator.Models.Royalty;
using PdfGenerator.Queries;
using System.Data;

namespace PdfGenerator.Data.Royalty;

public class RoyaltyRepo : IRoyaltyRepo
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public RoyaltyRepo(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<RoyaltyResponse>> GetRoyaltiesAsync(GetRoyaltyQuery query)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var dParams = GetParamsForSp(query);
        var royalties = await GetRoyaltiesAsync(dParams);
        return royalties;
    }

    private static DynamicParameters GetParamsForSp(GetRoyaltyQuery query)
    {
        var dParams = new DynamicParameters();
        dParams.Add("@Year", query.Filter.Year);
        dParams.Add("@AccountNumber", query.Filter.AccountNumber);

        return dParams;
    }

    private async Task<List<RoyaltyResponse>> GetRoyaltiesAsync(SqlMapper.IDynamicParameters dParams)
    {
        const string spName = "dbo.usp_GetRoyalties";
        var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

        using var connection = _sqlConnectionFactory.CreateConnection();

        await using var gr = await connection.QueryMultipleAsync(command);
        var royalties = gr.Read<RoyaltyResponse>().ToList();

        return royalties;
    }
}