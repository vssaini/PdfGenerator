using PdfGenerator.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Text.Json.Serialization;

namespace PdfGenerator.Components.Royalty
{
    public sealed class RoyaltyDocument : IDocument
    {
        public RoyaltyModel RoyaltyModel { get; set; }
        public int FontSize { get; set; }

        /// <summary>
        /// Gets or sets the PDF file name.
        /// </summary>
        public string FileName { get; set; }

        [JsonConstructor]
        public RoyaltyDocument(RoyaltyModel royaltyModel, int fontSize)
        {
            RoyaltyModel = royaltyModel;
            FontSize = fontSize;

            FileName = $"{RoyaltyModel.Account} - {RoyaltyModel.Artist} -{RoyaltyModel.Year}.pdf";
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontFamily(Fonts.Calibri).FontSize(FontSize));

                    page.Header().Element(ComposeHeader);
                    page.Footer().Element(ComposeFooter);

                    page.Content().Element(ComposeContent);
                });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span("As of: ").SemiBold();
                        text.Span($"{RoyaltyModel.AsOfDate:MM/dd/yy}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Run Date: ").SemiBold();
                        text.Span($"{RoyaltyModel.RunDate:MM/dd/yy}");
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.AlignCenter();
                        text.Span(RoyaltyModel.StatementTitle);
                        text.EmptyLine();
                        text.Span(RoyaltyModel.StatementSubTitle);
                        text.EmptyLine();
                        text.Span(RoyaltyModel.PrintedFromTitle);
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.AlignRight();
                        text.Span("Page: ");
                        text.CurrentPageNumber();
                    });
                });
            });
        }

        private void ComposeSubHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span("Artist: ").SemiBold();
                        text.Span(RoyaltyModel.Artist);
                    });
                });

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.AlignLeft();
                        text.Span("Account: ").SemiBold();
                        text.Span(RoyaltyModel.Account.ToString());
                    });
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.AlignLeft();
                        text.Span(RoyaltyModel.Account.ToString());
                        text.Span(" : ");
                        text.Span(RoyaltyModel.Artist);
                    });
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                //column.Spacing(5);

                column.Item().Element(ComposeSubHeader);
                column.Item().Element(ComposeTable);
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Country");
                    header.Cell().Element(CellStyle).Text("Period");
                    header.Cell().Element(CellStyle).AlignRight().Text("Catalog No.");
                    header.Cell().Element(CellStyle).AlignRight().Text("Units");
                    header.Cell().Element(CellStyle).AlignRight().Text("Rate");
                    header.Cell().Element(CellStyle).AlignRight().Text("Amount");
                    header.Cell().Element(CellStyle).AlignRight().Text("Cyc");
                    header.Cell().Element(CellStyle).AlignRight().Text("Afm Lia.");
                    header.Cell().Element(CellStyle).AlignRight().Text("Roy Base");
                    header.Cell().Element(CellStyle).AlignRight().Text("Roy Rate");
                    header.Cell().Element(CellStyle).AlignRight().Text("Type");
                    header.Cell().Element(CellStyle).AlignRight().Text("Lic. %");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in RoyaltyModel.Items)
                {
                    table.Cell().Element(CellStyle).AlignLeft().Text(item.Country);

                    var firstPeriodProcessed = false;
                    foreach (var pr in item.PeriodRows)
                    {
                        var periodTxt = $"{pr.Period.StartDate:MM/dd/yy} TO {pr.Period.EndDate:MM/dd/yy}";

                        if (firstPeriodProcessed)
                            table.Cell().ColumnSpan(2).Element(CellStyle).AlignRight().Text(periodTxt);
                        else
                            table.Cell().Element(CellStyle).AlignMiddle().Text(periodTxt);

                        AddItemRowsToTable(pr.Rows, table);
                        firstPeriodProcessed = true;
                    }

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.PaddingVertical(5);
                    }
                }
            });
        }

        private static void AddItemRowsToTable(List<RoyaltyRow> rows, TableDescriptor table)
        {
            var firstRowProcessed = false;

            foreach (var row in rows)
            {
                if (firstRowProcessed)
                    table.Cell().ColumnSpan(3).Element(CellStyle).AlignRight().Text(row.CatalogNumber);
                else
                    table.Cell().Element(CellStyle).AlignRight().Text(row.CatalogNumber);

                table.Cell().Element(CellStyle).AlignRight().Text(row.Units.ToString(CultureInfo.InvariantCulture));
                table.Cell().Element(CellStyle).AlignRight().Text(row.Rate.ToString(CultureInfo.InvariantCulture));
                table.Cell().Element(CellStyle).AlignRight().Text(row.Amount.ToString(CultureInfo.InvariantCulture));
                table.Cell().Element(CellStyle).AlignRight().Text(row.Cycle);
                table.Cell().Element(CellStyle).AlignRight().Text(row.AfmLia.ToString(CultureInfo.InvariantCulture));
                table.Cell().Element(CellStyle).AlignRight().Text(row.RoyaltyBase.ToString(CultureInfo.InvariantCulture));
                table.Cell().Element(CellStyle).AlignRight().Text(row.RoyaltyRate.ToString(CultureInfo.InvariantCulture));
                table.Cell().Element(CellStyle).AlignRight().Text(row.Type);
                table.Cell().Element(CellStyle).AlignRight().Text(row.LicPercentage.ToString(CultureInfo.InvariantCulture));

                firstRowProcessed = true;
            }

            // Sub-total period row
            var totalUnits = rows.Sum(r => r.Units).ToString("##,###");
            var totalAmount = rows.Sum(r => r.Amount).ToString("C");

            table.Cell().ColumnSpan(2).Element(CellStyle).AlignRight().Text("Subtotal Period");
            table.Cell().ColumnSpan(2).Element(CellStyle).AlignRight().Text($"{totalUnits}*");
            table.Cell().ColumnSpan(2).Element(CellStyle).AlignRight().Text($"{totalAmount}*");
            table.Cell().ColumnSpan(6).Element(CellStyle).AlignRight().Text("");

            static IContainer CellStyle(IContainer container)
            {
                return container.PaddingVertical(5);
            }
        }
    }
}
