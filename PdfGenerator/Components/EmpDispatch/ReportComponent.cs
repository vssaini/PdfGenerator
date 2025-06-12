using PdfGenerator.Extensions;
using PdfGenerator.Models.Reports.EmpDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace PdfGenerator.Components.EmpDispatch;

public class ReportComponent(List<EmpDispatchResponse> rows) : IComponent
{
    public void Compose(IContainer container)
    {
        container.PaddingVertical(20).Column(column =>
        {
            column.Spacing(5);

            for (var i = 0; i < rows.Count; i++)
            {
                if (i > 0)
                {
                    column.Item().Row(r => r.ConstantItem(20).PaddingTop(10));
                }

                var edh = rows[i];
                column.Item().Row(r => ComposeEmployer(r, edh.EmployerName));

                ComposeEmployerLocations(edh, column);

                column.Item().Row(r => ComposeEmployerTotalDispatched(r, edh));
            }

            column.Item().Row(r => ComposeTotalDispatched(r, rows));
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
        
    private static void ComposeEmployerLocations(EmpDispatchResponse edh, ColumnDescriptor column)
    {
        foreach (var loc in edh.Locations)
        {
            column.Item()
                .PaddingLeft(15)
                .PaddingVertical(8)
                .Text(loc.LocationName)
                .FontSize(12)
                .SemiBold()
                .Italic();

            ComposeLocationShows(loc, column);
        }
    }

    private static void ComposeLocationShows(EmpDispatchLocation loc, ColumnDescriptor column)
    {
        foreach (var show in loc.Shows)
        {
            ComposeShow(column, show.ShowName);

            foreach (var skill in show.SkillHistories)
            {
                // SKILL TABLE
                column.Item().Component(new SkillTableComponent(skill));
            }
        }
    }

    private static void ComposeShow(ColumnDescriptor column, string showName)
    {
        // SHOW
        column.Item()
            .PaddingLeft(20)
            .PaddingVertical(0)
            .Text($"Show Name: {showName}")
            .FontSize(12)
            .SemiBold();

        column.Item()
            .Width(6, Unit.Inch)
            .PaddingVertical(-5)
            .PaddingLeft(20)
            .LineHorizontal(1)
            .LineColor(Colors.Black);

        column.Item()
            .PaddingVertical(1);
    }

    private static void ComposeEmployerTotalDispatched(RowDescriptor row, EmpDispatchResponse edh)
    {
        row.RelativeItem()
            .PaddingLeft(15)
            .PaddingVertical(8)
            .Text($"Total Dispatched for {edh.EmployerName}: {edh.TotalDispatched}")
            .FontSize(12)
            .Bold();
    }

    private static void ComposeTotalDispatched(RowDescriptor row, IReadOnlyCollection<EmpDispatchResponse> rows)
    {
        int uniqueEmployerCount = rows
            .Select(e => e.EmployerName)
            .Distinct()
            .Count();

        if (uniqueEmployerCount == 1)
            return;

        var totalDispatched = rows.Sum(r => r.TotalDispatched);

        row.RelativeItem()
            .PaddingLeft(15)
            .PaddingVertical(8)
            .Text($"Total Dispatched for {uniqueEmployerCount} employers: {totalDispatched}")
            .FontSize(13)
            .Bold();
    }
}