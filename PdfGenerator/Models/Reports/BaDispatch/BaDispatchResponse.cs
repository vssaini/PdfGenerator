namespace PdfGenerator.Models.Reports.BaDispatch;

public class BaDispatchResponse
{
    public string LocationName { get; set; }
    public List<Employer> Employers { get; set; }
}

public class Employer
{
    public string EmployerName { get; set; }
    public List<Show> Shows { get; set; }
}

public class Show
{
    public Summary Summary { get; set; }
    public List<DispatchRow> DispatchRows { get; set; }
}

public class Summary
{
    public int RequestId { get; set; }
    public string Show { get; set; }
    
    /// <summary>
    /// Represents LocationSub field in the database.
    /// </summary>
    public string Location { get; set; }
    /// <summary>
    /// Represents Booth field in the database.
    /// </summary>
    public string Details { get; set; }

    public string Requestor { get; set; }
    
    public string ReportTo { get; set; }
    public string BusinessAssociate { get; set; }
}