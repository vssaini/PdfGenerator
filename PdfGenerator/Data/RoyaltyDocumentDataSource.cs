using Bogus;
using PdfGenerator.Models;

namespace PdfGenerator.Data
{
    internal sealed class RoyaltyDocumentDataSource
    {
        private static readonly Random Random = new();
        private readonly Faker _faker;

        public RoyaltyDocumentDataSource()
        {
            _faker = new Faker();
        }

        public RoyaltyModel GetRoyaltyDetails()
        {
            var royaltyItems = Enumerable
                .Range(1, 5)
                .Select(_ => GenerateRandomRoyaltyItem())
                .ToList();

            var startDate = new DateTime(1996, 01, 01);
            var endDate = new DateTime(1997, 06, 30);

            return new RoyaltyModel
            {
                AsOfDate = _faker.Date.Between(startDate, endDate),
                RunDate = _faker.Date.Between(startDate, endDate),

                StatementTitle = "Statement of Artist Royalties From Foreign Sales",
                StatementSubTitle = "First Six Months of 1997",
                PrintedFromTitle = "Printed from View",

                Artist = "PETER DUCHIN",
                Account = 001694,

                Items = royaltyItems,

                Year = endDate.Year
            };
        }

        private RoyaltyItem GenerateRandomRoyaltyItem()
        {
            var rows = Enumerable
                .Range(1, 4)
                .Select(_ => GenerateRandomRoyaltyRow())
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
            var catalogNumber = new[] { "005 002", "005 033" };
            var catalogSuffixes = new[] { "060017", "060017@" };

            return new RoyaltyRow
            {
                CatalogNumber = $"{catalogNumber[Random.Next(0, catalogNumber.Length)]} {catalogSuffixes[Random.Next(0, catalogSuffixes.Length)]}",
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
