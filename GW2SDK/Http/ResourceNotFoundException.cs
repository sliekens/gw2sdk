namespace GuildWars2.Http;

/// <summary>Thrown when a server returns a Not Found result (404).</summary>
[PublicAPI]
public sealed class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string? message)
        : base(message)
    {
    }

    public ResourceNotFoundException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
}
