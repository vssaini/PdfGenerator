using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models.Reports.BaDispatch;
using System.Data;

namespace PdfGenerator.Data.Reports.BaDispatch
{
    public class BaDispatchRepo : IBaDispatchRepo
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ILogService _logService;

        public BaDispatchRepo(ISqlConnectionFactory sqlConnectionFactory, ILogService logService)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _logService = logService;
        }

        public async Task<List<BaDispatchResponse>> GetBaDispatchResponsesAsync(BaDispatchFilter filter)
        {
            var baDispatchReports = GetDispatchReports(filter);
            var itemReqIds = baDispatchReports.Select(i => i.RequestId).ToList();

            _logService.LogInformation("Getting BA Dispatch report sub data from database");

            const string sql = "SELECT * FROM dbo.vw_BADispatchReport_Sub WHERE RequestID IN @itemReqIds";
            using var connection = _sqlConnectionFactory.CreateConnection();
            var subDispatchReports = await connection.QueryAsync<vw_BADispatchReport_Sub>(sql, new { itemReqIds });

            var baDispatchResponses = baDispatchReports
                .Select(item => new BaDispatchResponse
                {
                    Summary = item,
                    DispatchRows = GetDispatchRows(item.RequestId, subDispatchReports)
                })
                .ToList();

            return baDispatchResponses;
        }

        private List<Summary> GetDispatchReports(BaDispatchFilter filter)
        {
            _logService.LogInformation("Getting BA Dispatch reports from database");

            var dParams = GetParamsForSp(filter);
            var reports = GetDispatchReportsFromDb(dParams);

            var baDispatchReports = reports
                .Select(item => new Summary
                {
                    Location = item.Location,
                    Employer = item.Employer,

                    RequestId = item.RequestID,
                    Show = item.Show,

                    Requestor = item.Requestor,
                    ReportTo = $"{item.ReportToName} {item.ReportToPhone}",

                    BusinessAssociate = item.BA
                })
                .ToList();

            return baDispatchReports;
        }

        private static DynamicParameters GetParamsForSp(BaDispatchFilter filter)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@StartDate", filter.StartDate);
            dParams.Add("@EndDate", filter.EndDate);

            return dParams;
        }

        private IEnumerable<usp_BADispatchReport_ByLocation_Result> GetDispatchReportsFromDb(SqlMapper.IDynamicParameters dParams)
        {
            const string spName = "dbo.usp_BADispatchReport_ByLocation";
            var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

            using var connection = _sqlConnectionFactory.CreateConnection();

            using var gr = connection.QueryMultiple(command);
            var reports = gr.Read<usp_BADispatchReport_ByLocation_Result>().ToList();

            return reports;
        }

        private static List<DispatchRow> GetDispatchRows(int requestId, IEnumerable<vw_BADispatchReport_Sub> subDispatchReports)
        {
            return subDispatchReports
                .Where(x => x.RequestID == requestId)
                .OrderBy(x => x.ReportTime)
                .ThenBy(x => x.C_LastFirst)
                .Select(sdr => new DispatchRow
                {
                    ReportTime = sdr.ReportTime,
                    Skill = sdr.Skill,
                    WorkerName = sdr.C_LastFirst,
                    WorkerId = sdr.WorkerID,
                    Status = new StatusDto
                    {
                        Member = sdr.WorkerStatus,
                        Lor = sdr.CallByName ? "LOR" : ""
                    }
                })
                .ToList();
        }
    }
}