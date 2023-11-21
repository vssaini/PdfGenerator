namespace PdfGenerator.Models.Reports.BaDispatch
{
    public class BaDispatchResponse
    {
        public Summary Summary { get; set; }
        public List<DispatchRow> DispatchRows { get; set; }
    }
}