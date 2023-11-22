using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.BaDispatch
{
    public class ReportComponent : IComponent
    {
        private readonly Summary _summary;
        private readonly List<DispatchRow> _rows;

        public ReportComponent(Summary summary, List<DispatchRow> rows)
        {
            _summary = summary;
            _rows = rows;
        }

        public void Compose(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row => row.RelativeItem().Component(new SummaryComponent(_summary)));
                column.Item().Row(row => row.RelativeItem().Component(new TableComponent(_rows)));
            });
        }
    }
}