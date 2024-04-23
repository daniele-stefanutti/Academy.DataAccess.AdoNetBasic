namespace AdoNetBasic.Business.Services;

internal class BaseService
{
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Just a mock of a logging client for demonstration purposes
    /// </summary>
    public void LogError(string message) => ErrorMessage = message;
}
