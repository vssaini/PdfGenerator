namespace PdfGenerator.Models.Reports.Common
{
    public class DispatchFilter : PdfFilter
    {
        public int? EmployerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DispatchFilter(DateTime startDate, DateTime endDate, bool showPdfPreview = true, int? employerId = null)
        {
            if (startDate > endDate)
                throw new ArgumentException("StartDate must be less than EndDate.");

            StartDate = startDate;
            EndDate = endDate;
            ShowPdfPreview = showPdfPreview;
            EmployerId = employerId;
        }
    }
}