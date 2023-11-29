using PdfGenerator.Models.Membership;

namespace PdfGenerator.Contracts.Membership
{
    public interface IActiveMemberDocDataSource
    {
        Task<ActiveMemberReportModel> GetActiveMemberModelAsync();
    }
}