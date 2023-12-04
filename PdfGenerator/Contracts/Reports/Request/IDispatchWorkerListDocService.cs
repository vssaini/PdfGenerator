namespace PdfGenerator.Contracts.Reports.Request
{
    public interface IDispatchWorkerListDocService
    {
        Task GenerateDispatchWorkerListDocAsync(int requestId);
    }
}