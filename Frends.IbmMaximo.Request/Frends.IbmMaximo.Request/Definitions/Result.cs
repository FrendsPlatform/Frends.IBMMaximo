namespace Frends.IbmMaximo.Request.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// True if the request was successful. False otherwise.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; }

    /// <summary>
    /// Response message as JToken, if the request was successful.
    /// </summary>
    /// <example>{ some: "value" }</example>
    public dynamic Response { get; }

    /// <summary>
    /// Error message, if the request was not successful.
    /// </summary>
    /// <example>Error message</example>
    public string Error { get; }

    internal Result(bool success, dynamic response, string error)
    {
        Success = success;
        Response = response;
        Error = error;
    }
}
