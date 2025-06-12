namespace PdfGenerator.Models.Reports.Common;

public class DispatchFilter : PdfFilter
{
    public int? EmployerId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public DispatchFilter(DateTime startDate, DateTime endDate, int? employerId = null, bool showPdfPreview = true, bool isPreview = false)
    {
        if (startDate > endDate)
            throw new ArgumentException("StartDate must be less than EndDate.");

        StartDate = startDate;
        EndDate = endDate;
        EmployerId = employerId;

        ShowPdfPreview = showPdfPreview;
        IsPreview = isPreview;
    }
}