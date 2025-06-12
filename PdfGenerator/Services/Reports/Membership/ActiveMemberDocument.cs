using PdfGenerator.Components.Membership;
using PdfGenerator.Models.Reports.Membership;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Services.Reports.Membership
{
    public class ActiveMemberDocument(ActiveMemberReportModel model) : IDocument
    {
        private const int DefaultFontSize = 12;
        private const string DefaultFont = "Arial";

        private const string FallbackFont = "Microsoft PhagsPa";

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

            const float marginLr = 0.25f;
            const float marginTb = 0.00f;

            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);

                    page.MarginLeft(marginLr, Unit.Inch);
                    page.MarginRight(marginLr, Unit.Inch);
                    page.MarginTop(marginTb, Unit.Inch);
                    page.MarginBottom(marginTb, Unit.Inch);

                    page.DefaultTextStyle(pageStyle);

                    page.Content().Element(ComposeContent);
                });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                foreach (var member in model.ActiveMembers)
                {
                    column.Item().Component(new MemberComponent(member));
                }
            });
        }
    }
}