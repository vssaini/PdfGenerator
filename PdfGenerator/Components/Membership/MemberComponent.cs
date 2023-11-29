using PdfGenerator.Models.Membership;
using PdfGenerator.Properties;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Membership
{
    public class MemberComponent : IComponent
    {
        private readonly ActiveMember _member;

        public MemberComponent(ActiveMember member)
        {
            _member = member;
        }

        public void Compose(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Item().Element(ComposeFirstLine);
                column.Item().Element(ComposeSecondLine);
                column.Item().Element(ComposeThirdLine);
                column.Item().PaddingTop(25).Element(ComposeFourthLine);
            });
        }

        private void ComposeFirstLine(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantItem(300).Height(50).Text("");
                row.ConstantItem(150).Height(50).Text(_member.VoterStatus);
                row.ConstantItem(100).Height(50).Text(_member.WorkerId.ToString());
            });
        }

        private void ComposeSecondLine(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantItem(250).AlignCenter().Text(_member.Date);
                row.ConstantItem(150).Height(50).AlignRight().Text(_member.WorkerName);
            });
        }

        private static void ComposeThirdLine(IContainer container)
        {
            container.PaddingTop(-30).Row(row =>
            {
                row.RelativeItem().AlignRight().Text(Resources.MembershipCardIssuer);
                row.RelativeItem().PaddingLeft(20).Text(Resources.MembershipCity);
                row.RelativeItem().AlignLeft().Text(Resources.MembershipState);
            });
        }

        private static void ComposeFourthLine(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantItem(250).AlignCenter().Text(Resources.MembershipCardPresident);
                row.ConstantItem(150).Height(50).AlignRight().Text(Resources.MembershipCardSecretary);
            });
        }
    }
}