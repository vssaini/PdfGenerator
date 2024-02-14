using PdfGenerator.Models.Reports.EBoard;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.EBoard
{
    internal class TableComponent : IComponent
    {
        private readonly string _employerName;
        private readonly List<DispatchSumRow> _disSumRows;

        public TableComponent(string employerName, List<DispatchSumRow> disSumRows)
        {
            _employerName = employerName;
            _disSumRows = disSumRows;
        }

        public void Compose(IContainer container)
        {
            container.Element(ComposeTable);
        }

        private void ComposeTable(IContainer container)
        {
            const string headerBgColor = "#EFEBE9";
            const string cellBgColor = "#EDE7F6";
            const string footerBgColor = "#F5F5F5";

            container
                .PaddingHorizontal(20)
                .Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(.3f, Unit.Inch);
                    columns.ConstantColumn(.7f, Unit.Inch);
                    columns.ConstantColumn(2f, Unit.Inch);
                    columns.ConstantColumn(1.3f, Unit.Inch);
                    columns.RelativeColumn();
                    columns.ConstantColumn(.4f, Unit.Inch);
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().ColumnSpan(6).Element(CaptionStyle).Text(_employerName);
                    header.Cell().Element(CellStyle).Text("");
                    header.Cell().Element(CellStyle).Text("ID");
                    header.Cell().Element(CellLeftStyle).Text("Facility");
                    header.Cell().Element(CellLeftStyle).Text("Location");
                    header.Cell().Element(CellLeftStyle).Text("Show");
                    header.Cell().Element(CellStyle).Text("Disp");

                    IContainer CaptionStyle(IContainer headerContainer)
                    {
                        return headerContainer.DefaultTextStyle(x =>
                                x.Bold().FontSize(10))
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .MinHeight(15)
                            .PaddingVertical(5)
                            .PaddingLeft(5)
                            .AlignLeft()
                            .AlignMiddle();
                    }

                    IContainer CellStyle(IContainer headerContainer)
                    {
                        return headerContainer.DefaultTextStyle(x => x.SemiBold())
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(headerBgColor)
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .AlignCenter()
                            .AlignMiddle();
                    }

                    IContainer CellLeftStyle(IContainer headerContainer)
                    {
                        return headerContainer.DefaultTextStyle(x => x.SemiBold())
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(headerBgColor)
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .PaddingLeft(5)
                            .AlignLeft()
                            .AlignMiddle();
                    }
                });

                // step 3
                for (var i = 0; i < _disSumRows.Count; i++)
                {
                    var item = _disSumRows[i];

                    var slNo = Convert.ToString(_disSumRows.IndexOf(item) + 1);

                    table.Cell().Element(CellStyle).Text(slNo);
                    table.Cell().Element(CellStyle).Text(item.Id.ToString());
                    table.Cell().Element(CellLeftStyle).Text(item.Facility);
                    table.Cell().Element(CellLeftStyle).Text(item.Location);
                    table.Cell().Element(CellLeftStyle).Text(item.ShowName);
                    table.Cell().Element(CellStyle).Text(item.DispatchCount.ToString());

                    IContainer CellStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(i % 2 == 0 ? Colors.White : cellBgColor)
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .AlignCenter();
                    }

                    IContainer CellLeftStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(i % 2 == 0 ? Colors.White : cellBgColor)
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .PaddingLeft(5)
                            .AlignLeft();
                    }
                }

                // step 4
                //table.Footer(footer =>
                //{
                //    var footerTxt = $"Total Dispatches {_employerName}";
                //    var totalDispatches = _disSumRows.Sum(x => x.DispatchCount);

                //    footer.Cell().ColumnSpan(5).Element(FooterRightStyle).Text(footerTxt);
                //    footer.Cell().Element(FooterStyle).Text(totalDispatches.ToString());

                //    IContainer FooterRightStyle(IContainer footerContainer)
                //    {
                //        return footerContainer.DefaultTextStyle(x => x.SemiBold())
                //            .Border(1)
                //            .BorderColor(Colors.Black)
                //            .Background(footerBgColor)
                //            .MinHeight(15)
                //            .PaddingVertical(3)
                //            .PaddingRight(5)
                //            .AlignRight()
                //            .AlignMiddle();
                //    }

                //    IContainer FooterStyle(IContainer footerContainer)
                //    {
                //        return footerContainer.DefaultTextStyle(x => x.SemiBold())
                //            .Border(1)
                //            .BorderColor(Colors.Black)
                //            .Background(footerBgColor)
                //            .MinHeight(15)
                //            .PaddingVertical(3)
                //            .AlignCenter()
                //            .AlignMiddle();
                //    }
                //});
            });
        }
    }
}