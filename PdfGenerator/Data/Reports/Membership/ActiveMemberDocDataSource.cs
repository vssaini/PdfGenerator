using PdfGenerator.Contracts.Reports.Membership;
using PdfGenerator.Models.Reports.Membership;

namespace PdfGenerator.Data.Reports.Membership
{
    public sealed class ActiveMemberDocDataSource : IActiveMemberDocDataSource
    {
        private readonly IActiveMemberRepo _amRepo;

        public ActiveMemberDocDataSource(IActiveMemberRepo amRepo)
        {
            _amRepo = amRepo;
        }

        public async Task<ActiveMemberReportModel> GetActiveMemberModelAsync()
        {
            var activeMembers = await _amRepo.GetActiveMembersAsync(1); //.GetAllActiveMembersAsync();

            return new ActiveMemberReportModel
            {
                ActiveMembers = activeMembers
            };
        }
    }
}
