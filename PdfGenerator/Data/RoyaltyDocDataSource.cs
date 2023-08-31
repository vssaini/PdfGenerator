using PdfGenerator.Contracts;
using PdfGenerator.Models;
using PdfGenerator.Queries;

namespace PdfGenerator.Data
{
    public sealed class RoyaltyDocDataSource : IRoyaltyDocDataSource
    {
        private readonly IRoyaltyRepo _royRepo;
        private List<RoyaltyResponse> _royalties;
        private static readonly Random Random = new();

        private static DateTime _asOfDate;

        public RoyaltyDocDataSource(IRoyaltyRepo royRepo)
        {
            _royRepo = royRepo;
        }

        public async Task<RoyaltyModel> GetRoyaltyModelAsync(GetRoyaltyQuery query)
        {
            _royalties = await _royRepo.GetRoyaltiesAsync(query);
            var royaltyItems = GetRoyaltyItems();

            return new RoyaltyModel
            {
                AsOfDate = _asOfDate,
                RunDate = DateTime.Now,

                StatementTitle = "Statement of Artist Royalties From Foreign Sales",
                StatementSubTitle = $"First Six Months of {_asOfDate.Year}",
                PrintedFromTitle = "Printed from View",

                Artist = _royalties.Select(r => r.Artist).First(),
                Account = _royalties.Select(r => r.AccountNumber).First(),

                Items = royaltyItems,

                Year = _asOfDate.Year
            };
        }
        
        private List<RoyaltyItem> GetRoyaltyItems()
        {
            var royaltyItems = new List<RoyaltyItem>();

            var countryRoyalties = _royalties.GroupBy(r => r.Country);

            foreach (var cr in countryRoyalties)
            {
                var royItem = new RoyaltyItem { Country = cr.Key };

                var quarterRoyalties = cr.GroupBy(rr => ((rr.FromDate.Month - 1) / 3)).ToList();
                royItem.PeriodRows = quarterRoyalties.Select(GetPeriodRow).ToList();

                royaltyItems.Add(royItem);
            }

            return royaltyItems;
        }

        private static PeriodRow GetPeriodRow(IGrouping<int, RoyaltyResponse> qr)
        {
            // Not sorting by FromDate as already receiving sorted data from SP
            var endDate = _asOfDate = qr.OrderByDescending(r => r.ToDate).First().ToDate;

            var pr = new PeriodRow
            {
                Period = new Period(qr.First().FromDate, endDate),
                Rows = GetRoyaltyRows(qr)
            };
            pr.SubTotalPeriod = new SubTotalPeriod(pr.Rows.Sum(r => r.Units), pr.Rows.Sum(r => r.Amount));

            return pr;
        }

        private static List<RoyaltyRow> GetRoyaltyRows(IEnumerable<RoyaltyResponse> qr)
        {
            var catalogNumbers = new[] { "005 002", "005 033" };
            var catalogSuffixes = new[] { "060017", "060017@" };

            return qr.Select(r => new RoyaltyRow
            {
                CatalogNumber =
                        $"{catalogNumbers[Random.Next(0, catalogNumbers.Length)]} {catalogSuffixes[Random.Next(0, catalogSuffixes.Length)]}",
                Units = r.Units,
                Rate = (decimal)Math.Round(Random.NextDouble() * 100, 2),
                Amount = r.RoyaltyAmount,
                Cycle = r.StmtCycle,
                AfmLia = r.AfmLia,
                RoyaltyBase = r.RoyBase,
                RoyaltyRate = r.RoyRate,
                Type = "B01",
                LicPercentage = r.LicPercentage
            })
                .ToList();
        }
    }
}
