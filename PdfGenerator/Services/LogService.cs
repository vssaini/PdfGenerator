using PdfGenerator.Contracts;
using PdfGenerator.Models;
using Serilog;
using Serilog.Context;

namespace PdfGenerator.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger _logger = Log.Logger;
        private const string PropertyName = "UserName";
        private readonly string _currentUserName = Constants.Username;

        public void LogVerbose(string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.PushProperty(PropertyName, _currentUserName))
            {
                _logger.Verbose(messageTemplate, propertyValues);
            }
        }

        public void LogDebug(string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.PushProperty(PropertyName, _currentUserName))
            {
                _logger.Debug(messageTemplate, propertyValues);
            }
        }

        public void LogInformation(string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.PushProperty(PropertyName, _currentUserName))
            {
                _logger.Information(messageTemplate, propertyValues);
            }
        }

        public void LogWarning(string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.PushProperty(PropertyName, _currentUserName))
            {
                _logger.Warning(messageTemplate, propertyValues);
            }
        }

        public void LogError(Exception exc, string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.PushProperty(PropertyName, _currentUserName))
            {
                while (exc.InnerException != null)
                    exc = exc.InnerException;

                _logger.Error(exc, messageTemplate, propertyValues);
            }
        }

        public void LogError(string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.PushProperty(PropertyName, _currentUserName))
            {
                _logger.Error(messageTemplate, propertyValues);
            }
        }
    }
}