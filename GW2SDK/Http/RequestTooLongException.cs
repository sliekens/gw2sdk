using System;
using JetBrains.Annotations;

namespace GW2SDK.Http;

[PublicAPI]
public sealed class RequestTooLongException : InvalidOperationException
{
    public RequestTooLongException()
    {
    }

    public RequestTooLongException(string? message)
        : base(message)
    {
    }

    public RequestTooLongException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
