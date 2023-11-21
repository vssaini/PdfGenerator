using PdfGenerator.Components.BaDispatch;
using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Services.Reports.BaDispatch
{
    public class BaDispatchReportDocument : IDocument
    {
        private readonly BaDispatchReportModel _model;

        private const int DefaultFontSize = 9;
        private const string DefaultFont = "Arial";
        private const string ArialFont = "Arial";

        public BaDispatchReportDocument(BaDispatchReportModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            var pageStyle = TextStyle.Default
                .FontSize(DefaultFontSize)
                .FontFamily(DefaultFont)
                .FontColor(Colors.Grey.Darken4);

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
                .FontFamily(ArialFont)
                .SemiBold()
                .Italic();

            var dateStyle = TextStyle.Default
                .FontSize(12)
                .FontFamily(ArialFont)
                .SemiBold();

            container.Column(column =>
            {
                column.Item().Text(text => text.Span(_model.Header.Title).Style(titleStyle));
                column.Item().BorderBottom(1).PaddingVertical(2).PaddingBottom(5).Text(text => text.Span(_model.Header.DateRange).Style(dateStyle));
            });
        }

        private void ComposeContent(IContainer container)
        {
            int counter = 0;
            container.Column(column =>
            {
                foreach (var dispResp in _model.BaDispatchResponses)
                {
                    column.Item().Component(new ReportComponent(dispResp.Summary, dispResp.DispatchRows));

                    if (counter == 2)
                        break;

                    counter++;
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {
            var fontStyle = TextStyle.Default
                .FontSize(8)
                .FontFamily(ArialFont);

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

                row.RelativeItem().PaddingLeft(3).PaddingTop(5).Column(c =>
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