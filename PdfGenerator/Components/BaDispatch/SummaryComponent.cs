using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.BaDispatch;

internal class SummaryComponent(Summary summary) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().PaddingVertical(5).Row(ComposeSummary);
        });
    }

    private void ComposeSummary(RowDescriptor row)
    {
        var fontStyle = TextStyle.Default.FontSize(10);

        row.RelativeItem()
            .AlignLeft()
            .PaddingLeft(30)
            .Column(rColumn =>
            {
                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.Span("ID").Bold();
                        text.Span("   ");
                        text.Span(summary.RequestId.ToString());
                    });

                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.Span("Show").Bold();
                        text.Span("   ");
                        text.Span(summary.Show);
                    });
            });

        row.RelativeItem()
            .AlignCenter()
            .Column(rColumn =>
            {
                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.Span("Loc").Bold();
                        text.Span("   ");
                        text.Span(summary.Location);
                    });

                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.Span("Details").Bold();
                        text.Span("    ");
                        text.Span(TrimText(summary.Details, 25));
                    });
            });

        row.RelativeItem()
            .AlignRight()
            .PaddingRight(35)
            .Column(rColumn =>
            {
                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.AlignLeft();
                        text.Span("Report To").Bold();
                        text.Span("   ");
                        text.Span(summary.ReportTo);
                    });

                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.AlignLeft();
                        text.Span("BA").Bold();
                        text.Span("   ");
                        text.Span(summary.BusinessAssociate ?? "NA");
                    });
            });
    }
    
    private static string TrimText(string text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        return text.Length <= maxLength ? text : text[..maxLength] + "...";
    }
}