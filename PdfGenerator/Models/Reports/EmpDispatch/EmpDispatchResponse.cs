using PdfGenerator.Models.Reports.BaDispatch;

namespace PdfGenerator.Models.Reports.EmpDispatch
{
    public class EmpDispatchResponse
    {
        public Summary Summary { get; set; }
        public List<DispatchRow> DispatchRows { get; set; }
    }
}