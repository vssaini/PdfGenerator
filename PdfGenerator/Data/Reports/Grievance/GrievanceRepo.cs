using Dapper;
using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using System.Data;

namespace PdfGenerator.Data.Reports.Grievance
{
    public class GrievanceRepo(ISqlConnectionFactory sqlConnectionFactory, ILogger<GrievanceRepo> logger)
        : IGrievanceRepo
    {
        public async Task<GrievanceLetterStepOneResponse> GetGrievanceLetterStepOneAsync(GrievanceFilter filter)
        {
            var dParams = GetParamsForSp(filter);
            var grvLetStepOneResp = await GetGrievanceLetterStepOneAsync(dParams);

            return grvLetStepOneResp;
        }

        private static DynamicParameters GetParamsForSp(GrievanceFilter filter)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@GrievanceId", filter.GrievanceId);

            return dParams;
        }

        private async Task<GrievanceLetterStepOneResponse> GetGrievanceLetterStepOneAsync(SqlMapper.IDynamicParameters dParams)
        {
            const string spName = "dbo.usp_Grievance_Letter_StepOne";
            logger.LogInformation("Executing SP {SpName} to get grievance letter step one data.", spName);

            var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

            using var connection = sqlConnectionFactory.CreateConnection();

            await using var gr = await connection.QueryMultipleAsync(command);
            var grvLetStepOneResp = gr.Read<GrievanceLetterStepOneResponse>().FirstOrDefault();

            return grvLetStepOneResp;
        }
    }
}