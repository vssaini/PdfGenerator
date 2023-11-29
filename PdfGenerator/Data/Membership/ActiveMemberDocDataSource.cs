using PdfGenerator.Contracts.Membership;
using PdfGenerator.Models.Membership;

namespace PdfGenerator.Data.Membership
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
