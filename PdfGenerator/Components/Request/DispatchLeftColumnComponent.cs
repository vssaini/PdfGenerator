using PdfGenerator.Models.Reports.Request;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Request
{
    public class DispatchLeftColumnComponent(RequestHeaderVm dispatchSummary) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(2);
                column.Item().Element(ComposeTable);
            });
        }

        private void ComposeTable(IContainer container)
        {
            var fontStyle = TextStyle.Default
                .FontSize(9)
                .FontFamily("Arial");

            container
                .DefaultTextStyle(fontStyle)
                .Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);
                    columns.RelativeColumn();
                });


                table.Cell().Element(LeftCellStyle).Text("Employer");
                table.Cell().Element(RightCellStyle).Text(dispatchSummary.Employer);

                var reportDate = dispatchSummary.ReportAtTime.HasValue ? dispatchSummary.ReportAtTime.Value.ToString("ddddd, MMMMM dd, yyyy") : "";

                table.Cell().Element(LeftCellStyle).Text("Report Date");
                table.Cell().Element(RightCellStyle).Text(reportDate);

                table.Cell().Element(LeftCellStyle).Text("Report To");
                table.Cell().Element(RightCellStyle).Text(dispatchSummary.ReportToName);

                table.Cell().Element(LeftCellStyle).Text("Steward");
                table.Cell().Element(RightCellStyle).Text("");

                table.Cell().Element(LeftCellStyle).Text("Business Agent");
                table.Cell().Element(RightCellStyle).Text(dispatchSummary.BusinessAgent);

                table.Cell().Element(LeftCellStyle).Text("Call No.");
                table.Cell().Element(RightCellStyle).Text(dispatchSummary.CallNo);

                static IContainer LeftCellStyle(IContainer container)
                {
                    return container.AlignRight().PaddingRight(10).PaddingVertical(2);
                }

                static IContainer RightCellStyle(IContainer container)
                {
                    return container.AlignLeft().PaddingVertical(2);
                }
            });
        }
    }
}
