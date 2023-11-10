using PdfGenerator.Models.Grievance.LetterStepOne;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace PdfGenerator.Components.Grievance;

public class GrievanceStepOneDocument : IDocument
{
    private readonly int _fontSize;
    private readonly string _fontFamily;
    private readonly GrievanceLetterStepOneModel _model;

    private const string ArialFont = "Arial";

    public GrievanceStepOneDocument(GrievanceLetterStepOneModel model, int fontSize, string fontFamily)
    {
        _fontSize = fontSize;
        _fontFamily = fontFamily;
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        var pageStyle = TextStyle.Default.FontSize(_fontSize).FontFamily(_fontFamily).FontColor(Colors.Grey.Darken4);

        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(15);
                page.DefaultTextStyle(pageStyle);

                page.Header().Element(ComposeLetterHeading);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeLetterFooter);
            });
    }

    private void ComposeLetterHeading(IContainer container)
    {
        var fontStyle = TextStyle.Default.FontSize(11).FontFamily(ArialFont).SemiBold().Italic();

        container.PaddingVertical(20).Row(row =>
        {
            row.ConstantItem(100).Width(70).Image(_model.Heading.CompanyLogo);

            row.RelativeItem().AlignCenter().Text(text => text.Span(_model.Heading.Title).Style(fontStyle));

            row.ConstantItem(100).Width(100).AlignRight().Image(_model.Heading.LocalLogo);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Element(ComposeLetterAddress);
            column.Item().PaddingTop(10).PaddingBottom(10).Element(ComposeLetterSubject);
            column.Item().Element(ComposeLetterBody);
            column.Item().Element(ComposeLetterSignature);
            column.Item().Element(ComposeLetterCarbonCopy);
        });
    }

    private void ComposeLetterAddress(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Component(new AddressComponent(_model.Address));
        });
    }

    private void ComposeLetterSubject(IContainer container)
    {
        var fontStyle = TextStyle.Default.FontSize(12).SemiBold().Italic();

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.DefaultTextStyle(fontStyle);
                    text.AlignLeft();
                    text.Span("Re:");
                    text.Span("   ");
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
                    text.Line(_model.Signature.ComplementaryClose);
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
                column.Item().PaddingTop(25).Text(text =>
                {
                    text.Line(_model.Signature.WriterName);
                    text.Line(_model.Signature.WriterDesignation);
                });
            });
        });
    }

    private void ComposeLetterCarbonCopy(IContainer container)
    {
        var fontStyle = TextStyle.Default.SemiBold().Italic();

        container.PaddingTop(5).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Line($"Cc:    {_model.CarbonCopy.PersonOne}");
                    text.Line($"          {_model.CarbonCopy.PersonTwo}");
                    text.Line($"          {_model.CarbonCopy.PersonThree}");

                    text.Line(_model.CertifiedStatement).Style(fontStyle);
                });
            });
        });
    }

    private void ComposeLetterFooter(IContainer container)
    {
        var fontStyle = TextStyle.Default.FontSize(9).FontFamily(ArialFont);

        container.PaddingVertical(20).BorderTop(1).PaddingBottom(1).Row(row =>
        {
            row.RelativeItem().AlignLeft().PaddingTop(5).Column(c =>
            {
                c.Item().Text(_model.Footer.Telephone).Style(fontStyle);
                c.Item().Text(_model.Footer.Fax).Style(fontStyle);
            });

            row.RelativeItem().AlignCenter().PaddingTop(5).Column(c =>
            {
                c.Item().Text(_model.Footer.CompanyAddressFirstLine).Style(fontStyle);
                c.Item().Text(_model.Footer.CompanyAddressSecondLine).Style(fontStyle);
            });

            row.RelativeItem().AlignRight().PaddingTop(5).Column(c =>
            {
                c.Item().Text(_model.Footer.CompanyWebsite).Style(fontStyle);
            });
        });
    }
}