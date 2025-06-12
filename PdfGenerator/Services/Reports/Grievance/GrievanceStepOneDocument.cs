using PdfGenerator.Components.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace PdfGenerator.Services.Reports.Grievance;

public class GrievanceStepOneDocument(GrievanceLetterStepOneModel model) : IDocument
{
    private const int DefaultFontSize = 12;
    private const string DefaultFont = "Times New Roman";
    private const string ArialFont = "Arial";

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        var pageStyle = TextStyle.Default
            .FontSize(DefaultFontSize)
            .FontFamily(DefaultFont)
            .FontColor(Colors.Grey.Darken4);

        const float margin = 0.25f;

        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(margin, Unit.Inch);
                page.DefaultTextStyle(pageStyle);

                page.Header().Element(ComposeLetterHeading);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeLetterFooter);
            });
    }

    private void ComposeLetterHeading(IContainer container)
    {
        var fontStyle = TextStyle.Default
            .FontSize(11)
            .FontFamily(ArialFont)
            .SemiBold()
            .Italic();

        container.PaddingVertical(20).Row(row =>
        {
            row.ConstantItem(70).Width(70).Image(Image.FromFile(model.Header.CompanyLogoPath));

            row.RelativeItem().AlignCenter().Text(text => text.Span(model.Header.Title).Style(fontStyle));

            row.ConstantItem(90).Width(90).AlignRight().Image(Image.FromFile(model.Header.LocalLogoPath));
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
            row.RelativeItem().Component(new AddressComponent(model.Address));
        });
    }

    private void ComposeLetterSubject(IContainer container)
    {
        var fontStyle = TextStyle.Default
            .FontSize(DefaultFontSize)
            .SemiBold()
            .Italic();

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
                    text.Span(model.Subject);
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

                    text.Line(model.Body.FirstPara);
                    text.Line(model.Body.SecondPara);
                    text.Line(model.Body.ThirdPara);
                    text.Line(model.Body.ClosingPara);
                    text.Line(model.Signature.ComplementaryClose);
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
                    text.Line(model.Signature.WriterName);
                    text.Line(model.Signature.WriterDesignation);
                });
            });
        });
    }

    private void ComposeLetterCarbonCopy(IContainer container)
    {
        var fontStyle = TextStyle.Default
            .SemiBold()
            .Italic();

        container.PaddingTop(5).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Line($"Cc:    {model.CarbonCopy.PersonOne}");
                    text.Line($"          {model.CarbonCopy.PersonTwo}");
                    text.Line($"          {model.CarbonCopy.PersonThree}");

                    text.Line(" "); // Intentional space
                    text.Line(model.CertifiedStatement).Style(fontStyle);
                });
            });
        });
    }

    private void ComposeLetterFooter(IContainer container)
    {
        var fontStyle = TextStyle.Default
            .FontSize(9)
            .FontFamily(ArialFont);

        container.PaddingVertical(20).BorderTop(1).PaddingBottom(1).Row(row =>
        {
            row.RelativeItem().AlignLeft().PaddingTop(5).Column(c =>
            {
                c.Item().Text(model.Footer.Telephone).Style(fontStyle);
                c.Item().Text(model.Footer.Fax).Style(fontStyle);
            });

            row.RelativeItem().AlignCenter().PaddingTop(5).Column(c =>
            {
                c.Item().Text(model.Footer.CompanyAddressFirstLine).Style(fontStyle);
                c.Item().Text(model.Footer.CompanyAddressSecondLine).Style(fontStyle);
            });

            row.RelativeItem().AlignRight().PaddingTop(5).Column(c =>
            {
                c.Item().Text(model.Footer.CompanyWebsite).Style(fontStyle);
            });
        });
    }
}