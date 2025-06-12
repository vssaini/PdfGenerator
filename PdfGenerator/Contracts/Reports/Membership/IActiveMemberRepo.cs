using PdfGenerator.Models.Reports.Membership;

namespace PdfGenerator.Contracts.Reports.Membership;

public interface IActiveMemberRepo
{
    Task<List<ActiveMember>> GetActiveMembersAsync(int pageNumber);
    Task<List<ActiveMember>> GetAllActiveMembersAsync();
}