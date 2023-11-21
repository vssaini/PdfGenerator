namespace PdfGenerator.Models.Reports.BaDispatch
{
    public class BaDispatchFilter
    {
        public int? EmployerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}