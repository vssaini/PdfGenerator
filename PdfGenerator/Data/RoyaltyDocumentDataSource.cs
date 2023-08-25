using Bogus;
using PdfGenerator.Models;
using QuestPDF.Helpers;

namespace PdfGenerator.Data
{
    internal sealed class RoyaltyDocumentDataSource
    {
        private static readonly Random Random = new();
        private readonly Faker _faker;

        public RoyaltyDocumentDataSource()
        {
            _faker = new Faker("en_US");
        }

        public RoyaltyModel GetRoyaltyDetails()
        {
            var royaltyItems = Enumerable
                .Range(1, 5)
                .Select(i => GenerateRandomRoyaltyItem())
                .ToList();

            return new RoyaltyModel
            {
                AsOfDate = new DateTime(1997, 06, 30),
                RunDate = new DateTime(1997, 07, 18),

                StatementTitle = "Statement of Artist Royalties From Foreign Sales",
                StatementSubTitle = "First Six Months of 1997",
                PrintedFromTitle = "Printed from View",

                Artist = "PETER DUCHIN",
                Account = 001694,

                Items = royaltyItems
            };
        }

        private RoyaltyItem GenerateRandomRoyaltyItem()
        {
            var rows = Enumerable
                .Range(1, 3)
                .Select(i => GenerateRandomRoyaltyRow())
                .ToList();

            return new RoyaltyItem
            {
                Country = _faker.Address.Country(),
                Period = new Period(_faker.Date.PastDateOnly(27).ToDateTime(TimeOnly.MinValue), _faker.Date.PastDateOnly(27).ToDateTime(TimeOnly.MinValue)),
                Rows = rows,
                SubTotalPeriod = new SubTotalPeriod(rows.Sum(r => r.Units), rows.Sum(r => r.Amount))
            };
        }

        private static RoyaltyRow GenerateRandomRoyaltyRow()
        {
            return new RoyaltyRow
            {
                CatalogNumber = Placeholders.Label(),
                Units = Random.Next(-3, 4000),
                Rate = (decimal)Math.Round(Random.NextDouble() * 100, 2),
                Amount = (decimal)Math.Round(Random.NextDouble() * 100, 2),
                Cycle = "A97",
                AfmLia = 0.00,
                RoyaltyBase = (decimal)Math.Round(Random.NextDouble() * 100, 2),
                RoyaltyRate = (decimal)Math.Round(Random.NextDouble() * 100, 2),
                Type = "B01",
                LicPercentage = 0.2400
            };
        }
    }
}
