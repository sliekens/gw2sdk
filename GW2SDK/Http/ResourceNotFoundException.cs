namespace GuildWars2.Http;

/// <summary>Thrown when a server returns a Not Found result (404).</summary>
[PublicAPI]
public sealed class ResourceNotFoundException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="ResourceNotFoundException" /> class.</summary>
    public ResourceNotFoundException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ResourceNotFoundException" /> class with a specified error
    /// message.</summary> 
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ResourceNotFoundException(string? message)
        : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="ResourceNotFoundException" /> class with a specified error
    /// message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception
    /// is specified.</param>
    public ResourceNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
