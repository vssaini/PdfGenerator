using PdfGenerator.Extensions;
using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace PdfGenerator.Components.BaDispatch;

public class ReportComponent(List<BaDispatchResponse> rows) : IComponent
{
    public void Compose(IContainer container)
    {
        container.PaddingVertical(20).Column(column =>
        {
            column.Spacing(5);

            foreach (var bad in rows)
            {
                var bad1 = bad;
                column.Item().Row(r => ComposeLocation(r, bad1.LocationName));

                ComposeLocationEmployers(bad, column);
            }
        });
    }

    private static void ComposeLocation(RowDescriptor row, string locationName)
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
                    .Text(locationName)
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

    private static void ComposeLocationEmployers(BaDispatchResponse bad, ColumnDescriptor column)
    {
        foreach (var emp in bad.Employers)
        {
            column.Item()
                .PaddingLeft(30)
                .PaddingVertical(8)
                .Text(emp.EmployerName)
                .FontSize(12)
                .SemiBold()
                .Italic();

            ComposeEmployerShows(emp, column);
        }
    }

    private static void ComposeEmployerShows(Employer emp, ColumnDescriptor column)
    {
        foreach (var show in emp.Shows)
        {
            column.Item().Row(row => row.RelativeItem().Component(new SummaryComponent(show.Summary)));
            column.Item().Row(row => row.RelativeItem().Component(new TableComponent(show.DispatchRows)));
        }
    }
}