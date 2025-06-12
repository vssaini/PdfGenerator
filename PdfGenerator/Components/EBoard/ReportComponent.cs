using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.EBoard;

public class ReportComponent(string employerName, List<DispatchSumRow> disSumRows) : IComponent
{
    public void Compose(IContainer container)
    {
        container.PaddingVertical(20).Column(column =>
        {
            column.Spacing(5);

            column.Item().Row(row => row.RelativeItem().Component(new TableComponent(employerName, disSumRows)));
            column.Item().Row(row => row.RelativeItem().Component(new TableFooterComponent(employerName, disSumRows)));
        });
    }
}