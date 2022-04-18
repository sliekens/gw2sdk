using System;
using System.Net;
using JetBrains.Annotations;

namespace GW2SDK.Http;

/// <summary>Thrown when a server returns a failure result (5xx).</summary>
[PublicAPI]
public sealed class GatewayException : Exception
{
    public GatewayException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public GatewayException(HttpStatusCode statusCode, string? message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public GatewayException(HttpStatusCode statusCode, string? message, Exception? inner)
        : base(message, inner)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}
