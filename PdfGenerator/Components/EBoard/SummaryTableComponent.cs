using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.EBoard;

public class SummaryTableComponent(List<EmployerDispatch> empDispatches) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Element(ComposeTable);
    }

    private void ComposeTable(IContainer container)
    {
        container
            .PaddingHorizontal(20)
            .PaddingLeft(40)
            .PaddingTop(20)
            .Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(2f, Unit.Inch);
                columns.ConstantColumn(.4f, Unit.Inch);
            });

            foreach (var item in empDispatches)
            {
                table.Cell().Element(CellLeftStyle).Text(item.EmployerName);
                table.Cell().Element(CellStyle).Text(item.TotalDispatch.ToString());

                IContainer CellStyle(IContainer cellContainer)
                {
                    return cellContainer
                        .Border(1)
                        .BorderColor(Colors.Black)
                        .MinHeight(15)
                        .PaddingVertical(3)
                        .AlignCenter();
                }

                IContainer CellLeftStyle(IContainer cellContainer)
                {
                    return cellContainer
                        .Border(1)
                        .BorderColor(Colors.Black)
                        .MinHeight(15)
                        .PaddingVertical(3)
                        .PaddingLeft(5)
                        .AlignLeft();
                }
            }

            // Table footer
            table.Footer(footer =>
            {
                var totalDispatches = empDispatches.Sum(e => e.TotalDispatch);

                footer.Cell().Element(FooterRightStyle).Text("Total Dispatches");
                footer.Cell().Element(FooterStyle).Text(totalDispatches.ToString());

                IContainer FooterRightStyle(IContainer footerContainer)
                {
                    return footerContainer.DefaultTextStyle(x => x.SemiBold())
                        .Border(1)
                        .BorderColor(Colors.Black)
                        .MinHeight(15)
                        .PaddingVertical(3)
                        .PaddingRight(5)
                        .AlignRight()
                        .AlignMiddle();
                }

                IContainer FooterStyle(IContainer footerContainer)
                {
                    return footerContainer.DefaultTextStyle(x => x.SemiBold())
                        .Border(1)
                        .BorderColor(Colors.Black)
                        .MinHeight(15)
                        .PaddingVertical(3)
                        .AlignCenter()
                        .AlignMiddle();
                }
            });
        });
    }
}