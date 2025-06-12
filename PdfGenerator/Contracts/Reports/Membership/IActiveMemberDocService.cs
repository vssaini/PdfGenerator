namespace PdfGenerator.Contracts.Reports.Membership;

public interface IActiveMemberDocService
{
    Task GenerateActiveMemberDocAsync();
}