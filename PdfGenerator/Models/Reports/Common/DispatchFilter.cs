namespace PdfGenerator.Models.Reports.Common
{
    public class DispatchFilter
    {
        public int? EmployerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DispatchFilter(DateTime startDate, DateTime endDate, int? employerId = null)
        {
            EmployerId = employerId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}