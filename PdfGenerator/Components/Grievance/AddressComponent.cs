using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfGenerator.Components.Grievance
{
    public class AddressComponent(Address address) : IComponent
    {
        private readonly string _date = DateTime.Now.ToString("MMMM dd, yyyy");
        private Address Address { get; } = address;

        public void Compose(IContainer container)
        {
            var address = $"{Address.Address1}, {Address.Address2}".Trim().TrimEnd(',');

            container.Column(column =>
            {
                column.Spacing(2);

                column.Item().PaddingBottom(10).Text(_date);

                column.Item().Text(Address.Name);
                column.Item().Text(Address.Designation);
                column.Item().Text(Address.Employer);
                column.Item().Text(address);
                column.Item().Text(Address.CountryWithPinCode);
            });
        }
    }
}
