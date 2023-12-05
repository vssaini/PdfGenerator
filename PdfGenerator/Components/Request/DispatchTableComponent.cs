using PdfGenerator.Models.Reports.Request;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Request
{
    internal class DispatchTableComponent : IComponent
    {
        private readonly string _originalSkill;
        private readonly List<RequestWorkerListVm> _workers;
        private readonly bool _isLastRecord;

        private const int DefaultFontSize = 10;
        private const string DefaultFont = "Arial";

        public DispatchTableComponent(string originalSkill, List<RequestWorkerListVm> workers, bool isLastRecord)
        {
            _originalSkill = originalSkill;
            _workers = workers;
            _isLastRecord = isLastRecord;
        }

        public void Compose(IContainer container)
        {
            container.Element(ComposeTable);
        }

        private void ComposeTable(IContainer container)
        {
            // Ref - https://colorpicker.fr/

            var fontStyle = TextStyle.Default
                .FontSize(DefaultFontSize)
                .FontFamily(DefaultFont)
                .SemiBold();

            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(0.3f, Unit.Inch);
                    columns.ConstantColumn(0.8f, Unit.Inch);
                    columns.ConstantColumn(2.3f, Unit.Inch);
                    columns.ConstantColumn(0.6f, Unit.Inch);
                    columns.ConstantColumn(1.4f, Unit.Inch);
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell()
                        .ColumnSpan(6)
                        .Element(CellStyle)
                        .Text(t =>
                        {
                            t.DefaultTextStyle(fontStyle);
                            t.Span(_originalSkill);
                            t.AlignCenter();

                        }); //item.OriginalSkill

                    static IContainer CellStyle(IContainer headerContainer)
                    {
                        return headerContainer
                            .DefaultTextStyle(x => x.SemiBold().FontSize(10))
                            .BorderBottom(6)
                            .BorderColor(Colors.Black)
                            .Background("#ECECEC")
                            .MinHeight(10)
                            .PaddingVertical(4)
                            .AlignMiddle()
                            .AlignLeft()
                            .PaddingLeft(5);
                    }
                });


                // step 3
                for (var i = 0; i < _workers.Count; i++)
                {
                    var counter = i;
                    var item = _workers[i];

                    var slNo = _workers.IndexOf(item) + 1;

                    table.Cell().Element(CellStyle).AlignCenter().Text(slNo.ToString());
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.ReportTime);
                    table.Cell().Element(CellStyle).PaddingLeft(5).Text(item.WorkerName);
                    table.Cell().Element(CellStyle).Text(item.DispatchSkill);
                    table.Cell().Element(CellStyle).Text(item.Number);
                    table.Cell().Element(CellStyle).Text(item.EmailPersonal);

                    IContainer CellStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .BorderBottom(_isLastRecord ? 0.5f : 0)
                            .BorderColor(Colors.Black)
                            .Background(counter % 2 == 0 ? Colors.White : "#EFF2F7")
                            .PaddingVertical(3)
                            .DefaultTextStyle(t => t.FontSize(9));
                    }
                }
            });
        }
    }
}