using PdfGenerator.Models.Reports.BaDispatch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
                column.Item().Text(_summary.Employer).FontSize(13).Bold().Italic();
                column.Item().PaddingVertical(5).Row(ComposeSummary);
            });
        }

        private void ComposeEmployerLocation(RowDescriptor row)
        {
            row.ConstantItem(1).LineHorizontal(5).LineColor(Colors.Black);
            row.AutoItem().Border(1)
                .Padding(5)
                .Text(_summary.Location)
                .FontSize(13)
                .SemiBold()
                .Italic();
            row.ConstantItem(5).LineHorizontal(5).LineColor(Colors.Black);
        }

        private void ComposeSummary(RowDescriptor row)
        {
            var fontStyle = TextStyle.Default.FontSize(11);

            row.RelativeItem()
                .Column(rColumn =>
                {
                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignLeft();
                            text.Span("ID").Bold();
                            text.Span("   ");
                            text.Span(_summary.RequestId.ToString());
                        });

                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignLeft();
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
                            text.AlignLeft();
                            text.Span("Requestor").Bold();
                            text.Span("   ");
                            text.Span(_summary.Requestor);
                        });

                    rColumn.Item()
                        .Text(text =>
                        {
                            text.DefaultTextStyle(fontStyle);
                            text.AlignLeft();
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
                            text.Span(_summary.BusinessAssociate);
                        });
                });
        }
    }
}