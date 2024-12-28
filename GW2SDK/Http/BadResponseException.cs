namespace GuildWars2.Http;

/// <summary>Thrown when a server returns an unusable response.</summary>
[PublicAPI]
public sealed class BadResponseException : HttpRequestException
{
    /// <summary>Initializes a new instance of the <see cref="BadResponseException" /> class.</summary>
    public BadResponseException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="BadResponseException" /> class with a specified error message.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public BadResponseException(string? message)
        : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="BadResponseException" /> class with a specified error message and
    /// a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner
    /// exception is specified.</param>
    public BadResponseException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

#if NET
    /// <summary>Initializes a new instance of the <see cref="BadResponseException" /> class with a specified error message,
    /// a reference to the inner exception that is the cause of this exception, and the HTTP status code returned by the
    /// server.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner
    /// exception is specified.</param>
    /// <param name="statusCode">The HTTP status code returned by the server.</param>
    public BadResponseException(
        string? message,
        Exception? innerException,
        HttpStatusCode? statusCode
    )
        : base(message, innerException, statusCode)
    {
    }
#endif
}
