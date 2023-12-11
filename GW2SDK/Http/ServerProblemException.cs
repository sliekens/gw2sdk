using System.Net;

namespace GuildWars2.Http;

/// <summary>Thrown when a server returns a failure result (5xx).</summary>
[PublicAPI]
public sealed class ServerProblemException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="ServerProblemException" /> class with a specified status code.</summary>
    /// <param name="statusCode">The status code of the response which caused this exception.</param>
    public ServerProblemException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    /// <summary>Initializes a new instance of the <see cref="ServerProblemException" /> class with a specified status code and
    /// error message.</summary>
    /// <param name="statusCode">The status code of the response which caused this exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ServerProblemException(HttpStatusCode statusCode, string? message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    /// <summary>Initializes a new instance of the <see cref="ServerProblemException" /> class with a specified status code,
    /// error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="statusCode">The status code of the response which caused this exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner
    /// exception is specified.</param>
    public ServerProblemException(
        HttpStatusCode statusCode,
        string? message,
        Exception? innerException
    )
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    /// <summary>The status code of the response which caused this exception.</summary>
    public HttpStatusCode StatusCode { get; }
}
