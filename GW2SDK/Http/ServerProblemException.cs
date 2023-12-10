using System.Net;

namespace GuildWars2.Http;

/// <summary>Thrown when a server returns a failure result (5xx).</summary>
[PublicAPI]
public sealed class ServerProblemException : Exception
{
    public ServerProblemException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public ServerProblemException(HttpStatusCode statusCode, string? message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public ServerProblemException(HttpStatusCode statusCode, string? message, Exception? inner)
        : base(message, inner)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}
