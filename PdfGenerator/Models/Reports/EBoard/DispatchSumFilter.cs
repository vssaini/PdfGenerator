namespace PdfGenerator.Models.Reports.EBoard;

public class DispatchSumFilter
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public DispatchSumFilter(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}