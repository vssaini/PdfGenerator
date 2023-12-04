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

        private const int DefaultFontSize = 12;
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
                .FontSize(18)
                .FontFamily(DefaultFont)
                .SemiBold();

            container.Column(column =>
            {
                column.Item().AlignCenter().Text(_model.Header.Title).Style(titleStyle);
                column.Item().PaddingVertical(5).LineHorizontal(3).LineColor(Colors.Black);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(5).Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new DispatchDetailComponent());
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new DispatchDetailComponent());
                });

                column.Item().Element(ComposeTable);
                column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }

        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Product");
                    header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                    header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                    header.Cell().Element(CellStyle).AlignRight().Text("Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in _model.WorkerListModel.Workers)
                {
                    var slNo = _model.WorkerListModel.Workers.IndexOf(item) + 1;

                    table.Cell().Element(CellStyle).Text(slNo.ToString());
                    table.Cell().Element(CellStyle).Text(item.ReportTime);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.WorkerName);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.DispatchSkill);
                    table.Cell().Element(CellStyle).AlignRight().Text(item.PhoneType);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });

        }

        private void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14);
                column.Item().Text(Resources.DispatchWorkerList_Comment);
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