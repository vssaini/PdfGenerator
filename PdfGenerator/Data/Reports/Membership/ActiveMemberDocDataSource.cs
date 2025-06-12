using PdfGenerator.Contracts.Reports.Membership;
using PdfGenerator.Models.Reports.Membership;

namespace PdfGenerator.Data.Reports.Membership
{
    public sealed class ActiveMemberDocDataSource(IActiveMemberRepo amRepo) : IActiveMemberDocDataSource
    {
        public async Task<ActiveMemberReportModel> GetActiveMemberModelAsync()
        {
            var activeMembers = await amRepo.GetActiveMembersAsync(1); //.GetAllActiveMembersAsync();

            return new ActiveMemberReportModel
            {
                ActiveMembers = activeMembers
            };
        }
    }
}
