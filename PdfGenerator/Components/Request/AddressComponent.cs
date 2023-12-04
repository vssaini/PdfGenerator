using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Request
{
    public class DispatchDetailComponent : IComponent
    {
        public DispatchDetailComponent()
        {

        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(2);

                column.Item().Text(t => t.Span("Employer"));
                column.Item().Text(t => t.Span("Report Date"));
                column.Item().Text(t => t.Span("Report To"));
                column.Item().Text(t => t.Span("Steward"));
                column.Item().Text(t => t.Span("Business Agent"));
                column.Item().Text(t => t.Span("Call No."));
            });
        }
    }
}
