using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Address = PdfGenerator.Models.Grievance.LetterStepOne.Address;

namespace PdfGenerator.Components.Grievance
{
    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private Address Address { get; }

        public AddressComponent(string title, Address address)
        {
            Title = title;
            Address = address;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(2);

                column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

                column.Item().Text(Address.Name);
                column.Item().Text(Address.Designation);
                column.Item().Text($"{Address.Address1}, {Address.Address2}");
                column.Item().Text(Address.CountryWithPinCode);
            });
        }
    }
}
