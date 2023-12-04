using PdfGenerator.Models.Reports.Membership;

namespace PdfGenerator.Contracts.Reports.Membership
{
    public interface IActiveMemberDocDataSource
    {
        Task<ActiveMemberReportModel> GetActiveMemberModelAsync();
    }
}