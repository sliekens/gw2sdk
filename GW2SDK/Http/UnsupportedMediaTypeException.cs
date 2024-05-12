namespace GuildWars2.Http;

/// <summary>Thrown when the server returns content of an unsupported media type.</summary>
[PublicAPI]
public class UnsupportedMediaTypeException : InvalidOperationException
{
    /// <summary>Initializes a new instance of the <see cref="UnsupportedMediaTypeException" /> class.</summary>
    public UnsupportedMediaTypeException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="UnsupportedMediaTypeException" /> class with a specified error
    /// message.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public UnsupportedMediaTypeException(string message)
        : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="UnsupportedMediaTypeException" /> class with a specified error
    /// message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception
    /// is specified.</param>
    public UnsupportedMediaTypeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
