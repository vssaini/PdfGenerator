namespace PdfGenerator.Models.Reports.BaDispatch
{
    public class DispatchRow
    {
        public int SlNo { get; set; }
        public string ReportTime { get; set; }
        public string Skill { get; set; }
        public string WorkerName { get; set; }
        public int WorkerId { get; set; }
        public StatusDto Status { get; set; }
    }

    public class StatusDto
    {
        public string Member { get; set; }
        public string Lor { get; set; }
    }
}