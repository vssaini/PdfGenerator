using PdfGenerator.Models.Grievance.LetterStepOne;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace PdfGenerator.Components.Grievance;

public class GrievanceStepOneDocument : IDocument
{
    private readonly int _fontSize;
    private readonly GrievanceLetterStepOneModel _model;

    public GrievanceStepOneDocument(GrievanceLetterStepOneModel model, int fontSize)
    {
        _fontSize = fontSize;
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
                page.DefaultTextStyle(x => x.FontSize(_fontSize));

                page.Header().Element(ComposeLetterHeading);

                page.Content().Element(ComposeLetterAddress);
                page.Content().Element(ComposeLetterSubject);
                page.Content().Element(ComposeLetterBody);
                page.Content().Element(ComposeLetterSignature);

                page.Footer().Element(ComposeFooter);
            });
    }

    private void ComposeLetterHeading(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().Italic().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Width(100).Image(_model.Heading.CompanyLogo).FitArea();
                column.Item().Text(text => text.Span(_model.Heading.Title).Style(titleStyle));
                column.Item().Width(100).Image(_model.Heading.LocalLogo).FitArea();
            });
        });
    }

    private void ComposeLetterAddress(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Component(new AddressComponent("From", _model.Address));
        });
    }

    private void ComposeLetterSubject(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.AlignLeft();
                    text.Span("Re:");
                    text.Span(" ");
                    text.Span(_model.Subject);
                });
            });
        });
    }

    private void ComposeLetterBody(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.ParagraphSpacing(10);

                    text.Line(_model.Body.FirstPara);
                    text.Line(_model.Body.SecondPara);
                    text.Line(_model.Body.ThirdPara);
                    text.Line(_model.Body.ClosingPara);
                });
            });
        });
    }

    private void ComposeLetterSignature(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Span(_model.Signature.ComplementaryClose);
                });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {

    }
}