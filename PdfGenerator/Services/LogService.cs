using PdfGenerator.Contracts;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Constants = PdfGenerator.Models.Constants;

namespace PdfGenerator.Services;

public class LogService : ILogService
{
    private readonly ILogger _logger = Log.Logger;
    private const string PropertyName = "UserName";
    private const string CurrentUserName = Constants.Username;

    [MessageTemplateFormatMethod("messageTemplate")]
    public void LogVerbose(string messageTemplate, params object[] propertyValues)
    {
        using (LogContext.PushProperty(PropertyName, CurrentUserName))
        {
            _logger.Verbose(messageTemplate, propertyValues);
        }
    }

    [MessageTemplateFormatMethod("messageTemplate")]
    public void LogDebug(string messageTemplate, params object[] propertyValues)
    {
        using (LogContext.PushProperty(PropertyName, CurrentUserName))
        {
            _logger.Debug(messageTemplate, propertyValues);
        }
    }

    [MessageTemplateFormatMethod("messageTemplate")]
    public void LogInformation(string messageTemplate, params object[] propertyValues)
    {
        using (LogContext.PushProperty(PropertyName, CurrentUserName))
        {
            _logger.Information(messageTemplate, propertyValues);
        }
    }

    [MessageTemplateFormatMethod("messageTemplate")]
    public void LogWarning(string messageTemplate, params object[] propertyValues)
    {
        using (LogContext.PushProperty(PropertyName, CurrentUserName))
        {
            _logger.Warning(messageTemplate, propertyValues);
        }
    }

    [MessageTemplateFormatMethod("messageTemplate")]
    public void LogError(Exception exc, string messageTemplate, params object[] propertyValues)
    {
        using (LogContext.PushProperty(PropertyName, CurrentUserName))
        {
            while (exc.InnerException != null)
                exc = exc.InnerException;

            _logger.Error(exc, messageTemplate, propertyValues);
        }
    }

    [MessageTemplateFormatMethod("messageTemplate")]
    public void LogError(string messageTemplate, params object[] propertyValues)
    {
        using (LogContext.PushProperty(PropertyName, CurrentUserName))
        {
            _logger.Error(messageTemplate, propertyValues);
        }
    }
}