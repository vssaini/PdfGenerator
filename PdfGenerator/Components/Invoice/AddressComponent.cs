using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Address = PdfGenerator.Models.Invoice.Address;

namespace PdfGenerator.Components.Invoice
{
    public class AddressComponent(string title, Address address) : IComponent
    {
        private string Title { get; } = title;
        private Address Address { get; } = address;

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(2);

                column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

                column.Item().Text(Address.CompanyName);
                column.Item().Text(Address.Street);
                column.Item().Text($"{Address.City}, {Address.State}");
                column.Item().Text(Address.Email);
                column.Item().Text(Address.Phone);
            });
        }
    }
}
