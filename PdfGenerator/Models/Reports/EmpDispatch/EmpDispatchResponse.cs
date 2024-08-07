﻿namespace PdfGenerator.Models.Reports.EmpDispatch;

public class EmpDispatchResponse
{
    public string EmployerName { get; set; }
    public int TotalDispatched { get; set; }
    public List<EmpDispatchLocation> Locations { get; set; }
}

public class EmpDispatchLocation
{
    public string LocationName { get; set; }
    public List<EmpDispatchShow> Shows { get; set; }
}

public class EmpDispatchShow
{
    public string ShowName { get; set; }
    public List<EmpDispatchSkill> SkillHistories { get; set; }
}

public class EmpDispatchSkill
{
    public string SkillName { get; set; }
    public List<DispatchRow> DispatchRows { get; set; }
}