using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace PdfGenerator.Components.BaDispatch
{
    internal class SummaryComponent : IComponent
    {
        private readonly Summary _summary;

        public SummaryComponent(Summary summary)
        {
            _summary = summary;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Row(ComposeEmployerLocation);
                column.Item().PaddingLeft(30).PaddingVertical(8).Text(_summary.Employer).FontSize(12).SemiBold().Italic();
                column.Item().PaddingVertical(5).Row(ComposeSummary);
            });
        }

        private void ComposeEmployerLocation(RowDescriptor row)
        {
            const float lineSize = 2f;
            const float padVertical = 10f;

            row.ConstantItem(10)
                .PaddingVertical(padVertical)
                .LineHorizontal(lineSize)
                .LineColor(Colors.Black);

            row.AutoItem()
                //.MinimalBox()
                .Layers(layers =>
                {
                    layers.Layer().Canvas((canvas, size) =>
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
                        .Text(_summary.Location ?? "Allegiant Stadium Parking Lot")
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

        private void ComposeSummary(RowDescriptor row)
        {
            var fontStyle = TextStyle.Default.FontSize(10);

            row.RelativeItem()
                .AlignMiddle()
                .Column(rColumn =>
                {
                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignRight();
                            text.Span("ID").Bold();
                            text.Span("   ");
                            text.Span(_summary.RequestId.ToString());
                        });

                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignRight();
                            text.Span("Show").Bold();
                            text.Span("   ");
                            text.Span(_summary.Show);
                        });
                });

            row.RelativeItem()
                .Column(rColumn =>
                {
                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignRight();
                            text.Span("Requestor").Bold();
                            text.Span("   ");
                            text.Span(_summary.Requestor);
                        });

                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignRight();
                            text.Span("Report To").Bold();
                            text.Span("   ");
                            text.Span(_summary.Requestor);
                        });
                });

            row.RelativeItem()
                .Column(rColumn =>
                {
                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignRight();
                            text.Span("   ");
                        });

                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignRight();
                            text.Span("BA").Bold();
                            text.Span("   ");
                            text.Span(_summary.BusinessAssociate);
                        });
                });
        }
    }
}