using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
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

        public async Task<EmpDispatchResponse> GetEmpDispatchResponseAsync(DispatchFilter filter)
        {
            var dispatchReports = await GetDispatchReportsAsync(filter);
            return new EmpDispatchResponse { EmpDispatchHistories = dispatchReports };
        }

        private async Task<List<EmpDispatchHistory>> GetDispatchReportsAsync(DispatchFilter filter)
        {
            _logService.LogInformation("Getting Employer Dispatch reports from database");

            var dParams = GetParamsForSp(filter);
            var reports = await GetDispatchHistoriesFromDbAsync(dParams);

            return GetEmpDispatchHistories(reports);
        }

        private static DynamicParameters GetParamsForSp(DispatchFilter filter)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@StartDate", filter.StartDate);
            dParams.Add("@EndDate", filter.EndDate);

            return dParams;
        }

        private async Task<IEnumerable<usp_EmployerDispatchHistory_ByReportDate_Result>> GetDispatchHistoriesFromDbAsync(SqlMapper.IDynamicParameters dParams)
        {
            const string spName = "dbo.usp_EmployerDispatchHistory_ByReportDate";
            var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

            using var connection = _sqlConnectionFactory.CreateConnection();

            await using var gr = await connection.QueryMultipleAsync(command);
            var reports = gr.Read<usp_EmployerDispatchHistory_ByReportDate_Result>().ToList();

            return reports.Take(100);
        }

        private static List<EmpDispatchHistory> GetEmpDispatchHistories(IEnumerable<usp_EmployerDispatchHistory_ByReportDate_Result> dispatchHistories)
        {
            var empDisHistories = dispatchHistories
                .GroupBy(dh => dh.Employer)
                .Select(empGroup => new EmpDispatchHistory
                {
                    EmployerName = empGroup.Key,
                    Locations = empGroup
                        .GroupBy(dh => dh.Location)
                        .Select(locGroup => new EmpDispatchLocation
                        {
                            LocationName = locGroup.Key,
                            Shows = locGroup
                                .GroupBy(dh => dh.ShowName)
                                .Select(showGroup => new EmpDispatchShow
                                {
                                    ShowName = showGroup.Key,
                                    SkillHistories = showGroup
                                        .GroupBy(dh => dh.Skill)
                                        .Select(skillGroup => new EmpDispatchSkill
                                        {
                                            SkillName = skillGroup.Key,
                                            DispatchHistories = skillGroup
                                                .Select(x => new DispatchRow
                                                {
                                                    ReportDate = x.ReportAtTime.HasValue ? x.ReportAtTime.Value.ToShortDateString() : "NA",
                                                    ReportTime = x.ReportAtTime.HasValue ? x.ReportAtTime.Value.ToShortTimeString() : "NA",
                                                    WorkerId = x.WorkerID,
                                                    WorkerName = x.WorkerName
                                                })
                                                .OrderBy(x => x.WorkerName)
                                                .ToList()
                                        })
                                        .ToList()
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            return empDisHistories;
        }
    }
}