using Dapper;
using PdfGenerator.Contracts;
using PdfGenerator.Contracts.Reports.Request;
using PdfGenerator.Models.Reports.Request;

namespace PdfGenerator.Data.Reports.Request
{
    public class DispatchWorkerListRepo : IDispatchWorkerListRepo
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public DispatchWorkerListRepo(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<RequestWorkerListReportVm> GetDispatchWorkerAsync(int requestId)
        {
            const string sql = "SELECT * FROM dbo.vw_RequestHeader_html WHERE RequestID = @requestId;" +
                               "SELECT * FROM dbo.vw_Request_WorkerList WHERE RequestID2 = @requestId";

            using var connection = _sqlConnectionFactory.CreateConnection();
            await using var gr = await connection.QueryMultipleAsync(sql, new { requestId });

            var requestHeader = gr.Read<RequestHeaderVm>().FirstOrDefault();
            var workers = gr.Read<RequestWorkerListVm>().ToList();

            return new RequestWorkerListReportVm
            {
                RequestHeader = requestHeader,
                Workers = workers
            };
        }
    }
}