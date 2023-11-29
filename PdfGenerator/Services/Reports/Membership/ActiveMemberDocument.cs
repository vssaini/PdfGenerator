using PdfGenerator.Components.Membership;
using PdfGenerator.Models.Membership;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Services.Reports.Membership
{
    public class ActiveMemberDocument : IDocument
    {
        private readonly ActiveMemberReportModel _model;

        private const int DefaultFontSize = 9;
        private const string DefaultFont = "Arial";

        private const string FallbackFont = "Microsoft PhagsPa";

        public ActiveMemberDocument(ActiveMemberReportModel model)
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

                    page.Content().Element(ComposeContent);
                });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                foreach (var member in _model.ActiveMembers)
                {
                    column.Item().Component(new MemberComponent(member));
                    column.Item().PaddingBottom(30);
                }
            });
        }
    }
}