using Dapper;
using MoreLinq;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EmpDispatch;
using System.Data;

namespace PdfGenerator.Data.Reports.EmpDispatch
{
    public class EmpDispatchRepo : IEmpDispatchRepo
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ILogService _logService;

        public EmpDispatchRepo(ISqlConnectionFactory sqlConnectionFactory, ILogService logService)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _logService = logService;
        }

        public async Task<List<EmpDispatchResponse>> GetEmpDispatchResponsesAsync(DispatchFilter filter)
        {
            var dispatchReports = GetDispatchReports(filter);
            var itemReqIds = dispatchReports.Select(i => i.RequestId).ToList();

            var subDispatchReports = await GetSubDispatchReportsAsync(itemReqIds);

            var baDispatchResponses = dispatchReports
                .Select(item => new EmpDispatchResponse
                {
                    Summary = item,
                    DispatchRows = GetDispatchRows(item.RequestId, subDispatchReports)
                })
                .ToList();

            return baDispatchResponses;
        }

        private List<Summary> GetDispatchReports(DispatchFilter filter)
        {
            _logService.LogInformation("Getting Employer Dispatch reports from database");

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

        private static DynamicParameters GetParamsForSp(DispatchFilter filter)
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

        private async Task<List<vw_BADispatchReport_Sub>> GetSubDispatchReportsAsync(IEnumerable<int> itemReqIds)
        {
            _logService.LogInformation("Getting BA Dispatch report sub data from database");

            var subDispatchReports = new List<vw_BADispatchReport_Sub>();

            var itemReqIdsBatch = itemReqIds.Batch(2000).ToList();
            _logService.LogInformation($"Total {itemReqIdsBatch.Count} batches of RequestIds");

            foreach (var batchReqIds in itemReqIdsBatch)
            {
                const string sql = "SELECT * FROM dbo.vw_BADispatchReport_Sub WHERE RequestID IN @batchReqIds";
                using var connection = _sqlConnectionFactory.CreateConnection();
                var sdReports = await connection.QueryAsync<vw_BADispatchReport_Sub>(sql, new { batchReqIds });

                subDispatchReports.AddRange(sdReports);
            }

            return subDispatchReports;
        }

        private static List<DispatchRow> GetDispatchRows(int requestId, IEnumerable<vw_BADispatchReport_Sub> subDispatchReports)
        {
            return subDispatchReports
                .Where(x => x.RequestID == requestId)
                .OrderBy(x => x.ReportTime)
                .ThenBy(x => x._LastFirst)
                .Select(sdr => new DispatchRow
                {
                    ReportTime = sdr.ReportAtTime.ToString("hh:mm tt"),
                    Skill = sdr.Skill,
                    WorkerName = sdr._LastFirst,
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