namespace PdfGenerator.Contracts
{
    public interface ILogService
    {
        void LogVerbose(string messageTemplate, params object[] propertyValues);
        void LogDebug(string messageTemplate, params object[] propertyValues);
        void LogInformation(string messageTemplate, params object[] propertyValues);
        void LogWarning(string messageTemplate, params object[] propertyValues);

        void LogError(Exception exc, string messageTemplate, params object[] propertyValues);
        
        void LogError(string messageTemplate, params object[] propertyValues);
    }
}
