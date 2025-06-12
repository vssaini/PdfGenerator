using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.EBoard;

public class TableFooterComponent(string employerName, List<DispatchSumRow> disSumRows) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Element(ComposeTable);
    }

    private void ComposeTable(IContainer container)
    {
        const string footerBgColor = "#F5F5F5";

        container
            .PaddingHorizontal(20)
            .PaddingVertical(-4)
            .Table(table =>
        {
            // Columns
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(.3f, Unit.Inch);
                columns.ConstantColumn(.7f, Unit.Inch);
                columns.ConstantColumn(2f, Unit.Inch);
                columns.ConstantColumn(1.3f, Unit.Inch);
                columns.RelativeColumn();
                columns.ConstantColumn(.4f, Unit.Inch);
            });

            // Footer
            table.Footer(footer =>
            {
                var footerTxt = $"Total Dispatches {employerName}";
                var totalDispatches = disSumRows.Sum(x => x.DispatchCount);

                footer.Cell().ColumnSpan(5).Element(FooterRightStyle).Text(footerTxt);
                footer.Cell().Element(FooterStyle).Text(totalDispatches.ToString());

                IContainer FooterRightStyle(IContainer footerContainer)
                {
                    return footerContainer.DefaultTextStyle(x => x.SemiBold())
                        .Border(1)
                        .BorderTop(0)
                        .BorderColor(Colors.Black)
                        .Background(footerBgColor)
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
                        .BorderTop(0)
                        .BorderColor(Colors.Black)
                        .Background(footerBgColor)
                        .MinHeight(15)
                        .PaddingVertical(3)
                        .AlignCenter()
                        .AlignMiddle();
                }
            });
        });
    }
}