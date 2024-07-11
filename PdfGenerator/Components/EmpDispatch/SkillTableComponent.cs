using PdfGenerator.Models.Reports.EmpDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.EmpDispatch
{
    internal class SkillTableComponent : IComponent
    {
        private readonly EmpDispatchSkill _skill;

        public SkillTableComponent(EmpDispatchSkill skill)
        {
            _skill = skill;
        }

        public void Compose(IContainer container)
        {
            container.Element(ComposeTable);
        }

        private void ComposeTable(IContainer container)
        {
            container
                .PaddingLeft(30)
                .PaddingHorizontal(20)
                .PaddingVertical(5)
                .Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(.3f, Unit.Inch);
                    columns.ConstantColumn(.7f, Unit.Inch);
                    columns.ConstantColumn(.7f, Unit.Inch);
                    columns.RelativeColumn();
                    columns.ConstantColumn(.7f, Unit.Inch);
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().ColumnSpan(5).Element(CaptionStyle).Text(_skill.SkillName);

                    header.Cell().Element(CellStyle).Text("");
                    header.Cell().Element(CellStyle).Text("Date");
                    header.Cell().Element(CellStyle).Text("Time");
                    header.Cell().Element(CellStyle).Text("Worker Name");
                    header.Cell().Element(CellStyle).Text("ID");

                    IContainer CaptionStyle(IContainer headerContainer)
                    {
                        return headerContainer.DefaultTextStyle(x => x.SemiBold())
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .PaddingLeft(2)
                            .AlignLeft()
                            .AlignMiddle();
                    }

                    IContainer CellStyle(IContainer headerContainer)
                    {
                        return headerContainer.DefaultTextStyle(x => x.SemiBold())
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background("#EFEBE9")
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .AlignCenter()
                            .AlignMiddle();
                    }
                });

                // step 3
                for (var i = 0; i < _skill.DispatchHistories.Count; i++)
                {
                    var item = _skill.DispatchHistories[i];

                    var slNo = Convert.ToString(_skill.DispatchHistories.IndexOf(item) + 1);

                    table.Cell().Element(CellStyle).Text(slNo);
                    table.Cell().Element(CellStyle).Text(item.ReportDate);
                    table.Cell().Element(CellStyle).Text(item.ReportTime);
                    table.Cell().Element(CellLeftStyle).Text(item.WorkerName);
                    table.Cell().Element(CellStyle).Text(item.WorkerId.ToString());

                    IContainer CellStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(i % 2 == 0 ? Colors.White : "#EDE7F6")
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .AlignCenter();
                    }

                    IContainer CellLeftStyle(IContainer cellContainer)
                    {
                        return cellContainer
                            .Border(1)
                            .BorderColor(Colors.Black)
                            .Background(i % 2 == 0 ? Colors.White : "#EDE7F6")
                            .MinHeight(15)
                            .PaddingVertical(3)
                            .PaddingLeft(5)
                            .AlignLeft();
                    }
                }
            });
        }
    }
}