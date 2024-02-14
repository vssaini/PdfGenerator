using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.EBoard
{
    public class ReportComponent : IComponent
    {
        private readonly string _employerName;
        private readonly List<DispatchSumRow> _disSumRows;

        public ReportComponent(string employerName, List<DispatchSumRow> disSumRows)
        {
            _employerName = employerName;
            _disSumRows = disSumRows;
        }

        public void Compose(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row => row.RelativeItem().Component(new TableComponent(_employerName, _disSumRows)));
            });
        }
    }
}