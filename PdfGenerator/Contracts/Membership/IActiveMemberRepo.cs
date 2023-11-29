using PdfGenerator.Models.Membership;

namespace PdfGenerator.Contracts.Membership
{
    public interface IActiveMemberRepo
    {
        Task<List<ActiveMember>> GetActiveMembersAsync(int pageNumber);
        Task<List<ActiveMember>> GetAllActiveMembersAsync();
    }
}