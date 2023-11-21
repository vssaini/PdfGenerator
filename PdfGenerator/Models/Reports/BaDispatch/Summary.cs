namespace PdfGenerator.Models.Reports.BaDispatch
{
    public class Summary
    {
        public string Location { get; set; }
        public string Employer { get; set; }

        public int RequestId { get; set; }
        public string Show { get; set; }

        public string Requestor { get; set; }
        public string ReportTo { get; set; }

        public string BusinessAssociate { get; set; }
    }
}