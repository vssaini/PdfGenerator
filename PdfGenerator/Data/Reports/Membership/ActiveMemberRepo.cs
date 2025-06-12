using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Membership;
using PdfGenerator.Models.Reports.Membership;
using System.Data;

namespace PdfGenerator.Data.Reports.Membership;

public class ActiveMemberRepo(ISqlConnectionFactory sqlConnectionFactory) : IActiveMemberRepo
{
    public Task<List<ActiveMember>> GetActiveMembersAsync(int pageNumber)
    {
        const int pageSize = 10;

        var dParams = new DynamicParameters();
        dParams.Add("@PageNumber", pageNumber);
        dParams.Add("@PageSize", pageSize);
        dParams.Add("@FetchAll", false);

        return GetMembersAsync(dParams);
    }

    public Task<List<ActiveMember>> GetAllActiveMembersAsync()
    {
        var dParams = new DynamicParameters();
        dParams.Add("@FetchAll", true);

        return GetMembersAsync(dParams);
    }

    private async Task<List<ActiveMember>> GetMembersAsync(SqlMapper.IDynamicParameters dParams)
    {
        const string spName = "dbo.usp_GetActiveMembers";
        var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

        using var connection = sqlConnectionFactory.CreateConnection();
        await using var gr = await connection.QueryMultipleAsync(command);
        var activeMembers = gr.Read<ActiveMember>().ToList();

        return activeMembers;
    }
}