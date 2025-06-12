using Dapper;
using MoreLinq;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.BaDispatch;
using PdfGenerator.Models.Reports.BaDispatch;
using PdfGenerator.Models.Reports.Common;
using System.Data;
using DispatchRow = PdfGenerator.Models.Reports.BaDispatch.DispatchRow;

namespace PdfGenerator.Data.Reports.BaDispatch
{
    public class BaDispatchRepo(ISqlConnectionFactory sqlConnectionFactory, ILogService logService)
        : IBaDispatchRepo
    {
        public async Task<List<BaDispatchResponse>> GetBaDispatchResponsesAsync(DispatchFilter filter)
        {
            var reports = GetDispatchReportsFromDb(filter);

            var itemReqIds = reports.Select(i => i.RequestID);
            var subDispatchReports = await GetSubDispatchReportsFromDbAsync(itemReqIds);

            var baDispatchResponses = reports
                .OrderBy(dr => dr.Location)
                .GroupBy(dr => dr.Location)
                .Select(locationGroup => new BaDispatchResponse
                {
                    LocationName = locationGroup.Key,
                    Employers = locationGroup
                        .OrderBy(dr => dr.Employer)
                        .GroupBy(dr => dr.Employer)
                        .Select(employerGroup => new Employer
                        {
                            EmployerName = employerGroup.Key,
                            Shows = employerGroup
                                .OrderBy(x => x.Show)
                                .GroupBy(dr => dr.Show)
                                .Select(showGroup => new Show
                                {
                                    Summary = new Summary
                                    {
                                        RequestId = showGroup.First().RequestID,
                                        Show = showGroup.First().Show,
                                        Requestor = showGroup.First().Requestor,
                                        ReportTo = $"{showGroup.First().ReportToName} {showGroup.First().ReportToPhone}",
                                        BusinessAssociate = showGroup.First().BA
                                    },
                                    DispatchRows = GetDispatchRows(showGroup.First().RequestID, subDispatchReports)
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            return baDispatchResponses;
        }

        private List<usp_BADispatchReport_ByLocation_Result> GetDispatchReportsFromDb(DispatchFilter filter)
        {
            logService.LogInformation("Getting BA Dispatch reports from database");

            var dParams = GetParamsForSp(filter);

            const string spName = "dbo.usp_BADispatchReport_ByLocation";
            var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

            using var connection = sqlConnectionFactory.CreateConnection();

            using var gr = connection.QueryMultiple(command);
            var reports = gr.Read<usp_BADispatchReport_ByLocation_Result>().ToList();

            reports.ForEach(r => r.Show = r.Show?.Trim());
            return reports;
        }

        private static DynamicParameters GetParamsForSp(DispatchFilter filter)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@StartDate", filter.StartDate);
            dParams.Add("@EndDate", filter.EndDate);

            return dParams;
        }

        private async Task<List<vw_BADispatchReport_Sub>> GetSubDispatchReportsFromDbAsync(IEnumerable<int> itemReqIds)
        {
            logService.LogInformation("Getting BA Dispatch report sub data from database");

            var subDispatchReports = new List<vw_BADispatchReport_Sub>();

            var itemReqIdsBatch = itemReqIds.Batch(2000).ToList();
            logService.LogInformation($"Total {itemReqIdsBatch.Count} batches of RequestIds");

            foreach (var batchReqIds in itemReqIdsBatch)
            {
                const string sql = "SELECT * FROM dbo.vw_BADispatchReport_Sub WHERE RequestID IN @batchReqIds";
                using var connection = sqlConnectionFactory.CreateConnection();
                var sdReports = await connection.QueryAsync<vw_BADispatchReport_Sub>(sql, new { batchReqIds });

                subDispatchReports.AddRange(sdReports);
            }

            return subDispatchReports;
        }

        private static List<DispatchRow> GetDispatchRows(int requestId, IEnumerable<vw_BADispatchReport_Sub> subDispatchReports)
        {
            return subDispatchReports
                .Where(x => x.RequestID == requestId)
                .OrderBy(x => x.ReportAtTime)
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