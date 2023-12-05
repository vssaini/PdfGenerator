using PdfGenerator.Models.Reports.Request;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Request
{
    public class DispatchRightColumnComponent : IComponent
    {
        private readonly RequestHeaderVm _dispatchSummary;

        public DispatchRightColumnComponent(RequestHeaderVm dispatchSummary)
        {
            _dispatchSummary = dispatchSummary;
        }

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
                    columns.ConstantColumn(90);
                    columns.RelativeColumn();
                });


                table.Cell().Element(LeftCellStyle).Text("Request No.");
                table.Cell().Element(RightCellStyle).Text(_dispatchSummary.RequestID.ToString());

                table.Cell().Element(LeftCellStyle).Text("Workers Requested");
                table.Cell().Element(RightCellStyle).Text(Convert.ToString(_dispatchSummary.WorkersRequested));

                table.Cell().Element(LeftCellStyle).Text("Workers Assigned");
                table.Cell().Element(RightCellStyle).Text(Convert.ToString(_dispatchSummary.WorkersDispatched));

                table.Cell().Element(LeftCellStyle).Text("Facility");
                table.Cell().Element(RightCellStyle).Text(_dispatchSummary.Location);

                table.Cell().Element(LeftCellStyle).Text("Location");
                table.Cell().Element(RightCellStyle).Text(_dispatchSummary.Booth);

                table.Cell().Element(LeftCellStyle).Text("Show");
                table.Cell().Element(RightCellStyle).Text(_dispatchSummary.Show);

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
