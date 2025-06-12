using PdfGenerator.Models.Reports.Membership;
using PdfGenerator.Properties;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Membership;

public class MemberComponent(ActiveMember member) : IComponent
{
    public void Compose(IContainer container)
    {
        container.PaddingVertical(20).Column(column =>
        {
            column.Item().Element(ComposeFirstLine);
            column.Item().Element(ComposeSecondLine);
            column.Item().Element(ComposeThirdLine);
            column.Item().PaddingTop(25).Element(ComposeFourthLine);
        });
    }

    private void ComposeFirstLine(IContainer container)
    {
        container.PaddingBottom(20).Row(row =>
        {
            row.ConstantItem(300).Text("");
            row.ConstantItem(150).Text(member.VoterStatus);
            row.ConstantItem(100).Text(member.WorkerId.ToString());
        });
    }

    private void ComposeSecondLine(IContainer container)
    {
        container.Row(row =>
        {
            row.ConstantItem(150).AlignRight().Text(member.Date);
            row.ConstantItem(120).Text("");
            row.RelativeItem().AlignLeft().PaddingLeft(45).Text(member.WorkerName);
        });
    }

    private static void ComposeThirdLine(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().AlignRight().PaddingRight(98).Text(text =>
            {
                text.Span(Resources.MembershipCardIssuer);
                text.Span("    ");
                text.Span(Resources.MembershipCity);
                text.Span("    ");
                text.Span(Resources.MembershipState);
            });
        });
    }

    private static void ComposeFourthLine(IContainer container)
    {
        container.Row(row =>
        {
            row.ConstantItem(110).AlignRight().Text("");
            row.RelativeItem().AlignRight().Text(Resources.MembershipCardPresident);
            row.RelativeItem().AlignLeft().PaddingLeft(50).Text(Resources.MembershipCardSecretary);
        });
    }
}