using PdfGenerator.Extensions;
using PdfGenerator.Models.Reports.EmpDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace PdfGenerator.Components.EmpDispatch
{
    public class ReportComponent : IComponent
    {
        private readonly List<EmpDispatchHistory> _rows;

        public ReportComponent(List<EmpDispatchHistory> rows)
        {
            _rows = rows;
        }

        public void Compose(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(5);

                foreach (var edh in _rows)
                {
                    // EMPLOYER
                    column.Item().Row(r => ComposeEmployer(r, edh.EmployerName));

                    foreach (var loc in edh.Locations)
                    {
                        // LOCATION
                        column.Item()
                            .PaddingLeft(30)
                            .PaddingVertical(8)
                            .Text(loc.LocationName)
                            .FontSize(12)
                            .SemiBold()
                            .Italic();

                        foreach (var show in loc.Shows)
                        {
                            // SHOW
                            column.Item()
                                .PaddingLeft(30)
                                .PaddingVertical(8)
                                .Text(show.ShowName)
                                .FontSize(12)
                                .SemiBold()
                                .Underline();

                            foreach (var skill in show.SkillHistories)
                            {
                                // SKILL TABLE
                                column.Item().Component(new SkillTableComponent(skill));
                            }
                        }
                    }
                }
            });
        }

        private static void ComposeEmployer(RowDescriptor row, string empName)
        {
            const float lineSize = 2f;
            const float padVertical = 10f;

            row.ConstantItem(10)
                .PaddingVertical(padVertical)
                .LineHorizontal(lineSize)
                .LineColor(Colors.Black);

            row.AutoItem()
                .Layers(layers =>
                {
                    layers.Layer().SkiaSharpCanvas((canvas, size) =>
                    {
                        DrawRectangle(Colors.Black, true);

                        void DrawRectangle(string color, bool isStroke)
                        {
                            using var paint = new SKPaint
                            {
                                Color = SKColor.Parse(color),
                                IsStroke = isStroke,
                                StrokeWidth = 1,
                                IsAntialias = true
                            };

                            canvas.DrawRect(0, 0, size.Width, size.Height - 4, paint);
                        }
                    });

                    layers
                        .PrimaryLayer()
                        .Width(6, Unit.Inch)
                        .Height(0.3f, Unit.Inch)
                        .AlignMiddle()
                        .PaddingLeft(5)
                        .PaddingBottom(2)
                        .Text(empName)
                        .FontColor(Colors.Black)
                        .FontSize(12)
                        .SemiBold()
                        .Italic();
                });

            row.RelativeItem()
                .PaddingVertical(padVertical)
                .LineHorizontal(lineSize)
                .LineColor(Colors.Black);
        }
    }
}