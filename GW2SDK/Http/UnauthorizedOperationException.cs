namespace GuildWars2.Http;

/// <summary>Thrown when the server returns an Unauthorized or Forbidden result (401 or 403).</summary>
[PublicAPI]
public sealed class UnauthorizedOperationException : InvalidOperationException
{
    /// <summary>Initializes a new instance of the <see cref="UnauthorizedOperationException" /> class.</summary>
    public UnauthorizedOperationException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="UnauthorizedOperationException" /> class with a specified error
    /// message.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public UnauthorizedOperationException(string? message)
        : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="UnauthorizedOperationException" /> class with a specified error
    /// message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public UnauthorizedOperationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
