using PdfGenerator.Components.Request;
using PdfGenerator.Models.Reports.Request;
using PdfGenerator.Properties;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Services.Reports.Request
{
    public class DispatchWorkerListDocument : IDocument
    {
        private readonly DispatchWorkerListReportModel _model;

        private const int DefaultFontSize = 10;
        private const string DefaultFont = "Arial";

        private const string FallbackFont = "Microsoft PhagsPa";

        public DispatchWorkerListDocument(DispatchWorkerListReportModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            var fallbackStyle = TextStyle.Default
                .FontSize(DefaultFontSize)
                .FontFamily(FallbackFont)
                .FontColor(Colors.Grey.Darken4);

            var pageStyle = TextStyle.Default
                .FontSize(DefaultFontSize)
                .FontFamily(DefaultFont)
                .FontColor(Colors.Grey.Darken4)
                .Fallback(fallbackStyle);

            const float margin = 0.25f;

            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(margin, Unit.Inch);
                    page.DefaultTextStyle(pageStyle);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);
                });
        }

        private void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default
                .FontSize(16)
                .FontFamily(DefaultFont)
                .SemiBold();

            container.Column(column =>
            {
                column.Item().AlignCenter().Text(_model.Header.Title).Style(titleStyle);
                column.Item().PaddingVertical(5).LineHorizontal(2).LineColor(Colors.Black);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(5).Column(column =>
            {
                column.Spacing(5);

                column.Item().PaddingBottom(10).Row(row =>
                {
                    row.RelativeItem().Component(new DispatchLeftColumnComponent(_model.DispatchSummary));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new DispatchRightColumnComponent(_model.DispatchSummary));
                });

                ComposeTables(column);
                column.Item().PaddingTop(10).Element(ComposeComments);
            });
        }

        private void ComposeTables(ColumnDescriptor column)
        {
            var workersGrp = _model.Workers
                .GroupBy(w => w.OriginalSkil)
                .Select(g => new
                {
                    OriginalSkill = g.Key,
                    Workers = g.ToList()
                })
                .ToList();

            for (var i = 0; i < workersGrp.Count; i++)
            {
                var isLastRecord = i == workersGrp.Count - 1;

                var item = workersGrp[i];
                column.Item().Row(row => row.RelativeItem().Component(new DispatchTableComponent(item.OriginalSkill, item.Workers, isLastRecord)));
            }
        }

        private static void ComposeComments(IContainer container)
        {
            var fontStyle = TextStyle.Default
                .FontSize(9)
                .FontFamily(DefaultFont)
                .Italic();

            container.Column(column =>
            {
                column.Item().Text(t =>
                {
                    t.DefaultTextStyle(fontStyle);
                    t.Element().Text(Resources.DispatchWorkerListComment);
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {
            var fontStyle = TextStyle.Default
                .FontSize(8)
                .FontFamily(DefaultFont);

            container.PaddingVertical(20).BorderTop(1).PaddingBottom(1).Row(row =>
            {
                row.RelativeItem().PaddingTop(5).Column(c =>
                {
                    c.Item().Text(_model.Footer.CurrentUserName).Style(fontStyle);
                });

                row.RelativeItem().PaddingTop(5).Column(c =>
                {
                    c.Item().Text(_model.Footer.CurrentDateTime).Style(fontStyle);
                });

                row.RelativeItem().PaddingLeft(50).PaddingTop(5).Column(c =>
                {
                    c.Item().Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.Span("Page ");
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                    });
                });

                row.RelativeItem().PaddingTop(5).AlignRight().Column(c =>
                {
                    c.Item().Text(_model.Footer.PropertyMessage).Style(fontStyle);
                });
            });
        }
    }
}