﻿using PdfGenerator.Models.Reports.BaDispatch;
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
                        text.Span("Requestor").Bold();
                        text.Span("   ");
                        text.Span(summary.Requestor);
                    });

                rColumn.Item()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(fontStyle);
                        text.Span("Report To").Bold();
                        text.Span("    ");
                        text.Span(summary.Requestor);
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
                        text.Span("   ");
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
}