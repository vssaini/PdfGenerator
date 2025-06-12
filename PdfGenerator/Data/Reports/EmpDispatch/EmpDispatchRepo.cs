using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.EmpDispatch;
using PdfGenerator.Models.Reports.Common;
using PdfGenerator.Models.Reports.EmpDispatch;
using System.Data;

namespace PdfGenerator.Data.Reports.EmpDispatch;

public class EmpDispatchRepo(ISqlConnectionFactory sqlConnectionFactory, ILogService logService)
    : IEmpDispatchRepo
{
    public async Task<List<EmpDispatchResponse>> GetEmpDispatchResponsesAsync(DispatchFilter filter)
    {
        logService.LogInformation("Getting Employer Dispatch reports from database");

        var dParams = GetParamsForSp(filter);
        var reports = await GetDispatchHistoriesFromDbAsync(dParams);

        return GetEmpDispatchResponses(reports);
    }

    private static DynamicParameters GetParamsForSp(DispatchFilter filter)
    {
        var dParams = new DynamicParameters();
        dParams.Add("@StartDate", filter.StartDate);
        dParams.Add("@EndDate", filter.EndDate);
        dParams.Add("@IsPreview", filter.IsPreview);

        return dParams;
    }

    private async Task<IEnumerable<usp_EmployerDispatchHistory_ByReportDate_Result>> GetDispatchHistoriesFromDbAsync(SqlMapper.IDynamicParameters dParams)
    {
        const string spName = "dbo.usp_EmployerDispatchHistory_ByReportDate";
        var command = new CommandDefinition(spName, dParams, commandType: CommandType.StoredProcedure);

        using var connection = sqlConnectionFactory.CreateConnection();

        await using var gr = await connection.QueryMultipleAsync(command);
        var reports = gr.Read<usp_EmployerDispatchHistory_ByReportDate_Result>().ToList();

        return reports;
    }

    private static List<EmpDispatchResponse> GetEmpDispatchResponses(IEnumerable<usp_EmployerDispatchHistory_ByReportDate_Result> dispatchHistories)
    {
        var empDisHistories = dispatchHistories
            .OrderBy(dh => dh.Employer)
            .GroupBy(dh => dh.Employer)
            .Select(empGroup => new EmpDispatchResponse
            {
                EmployerName = empGroup.Key,
                TotalDispatched = empGroup.Count(),
                Locations = empGroup
                    .OrderBy(dh => dh.Location)
                    .GroupBy(dh => dh.Location)
                    .Select(locGroup => new EmpDispatchLocation
                    {
                        LocationName = locGroup.Key,
                        Shows = locGroup
                            .OrderBy(dh => dh.ShowName)
                            .GroupBy(dh => dh.ShowName)
                            .Select(showGroup => new EmpDispatchShow
                            {
                                ShowName = showGroup.Key,
                                SkillHistories = showGroup
                                    .OrderBy(dh => dh.Skill)
                                    .GroupBy(dh => dh.Skill)
                                    .Select(skillGroup => new EmpDispatchSkill
                                    {
                                        SkillName = skillGroup.Key,
                                        DispatchRows = skillGroup
                                            .OrderBy(dh => dh.ReportAtTime)
                                            .ThenBy(dh => dh.WorkerName)
                                            .Select(x => new DispatchRow
                                            {
                                                ReportDate = x.ReportAtTime.HasValue ? x.ReportAtTime.Value.ToShortDateString() : "NA",
                                                ReportTime = x.ReportAtTime.HasValue ? x.ReportAtTime.Value.ToShortTimeString() : "NA",
                                                WorkerId = x.WorkerID,
                                                WorkerName = x.WorkerName
                                            })
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