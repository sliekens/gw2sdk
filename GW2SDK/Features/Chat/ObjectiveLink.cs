namespace GuildWars2.Chat;

/// <summary>Represents a WvW objective chat link.</summary>
[PublicAPI]
public sealed record ObjectiveLink : Link
{
    /// <summary>The objective ID.</summary>
    public required int ObjectiveId { get; init; }

    /// <summary>The map ID.</summary>
    public required int MapId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[9]);
        buffer.WriteUInt8(LinkHeader.WvWObjective);
        buffer.WriteUInt24(ObjectiveId);
        buffer.Padding(1);
        buffer.WriteUInt24(MapId);
        buffer.Padding(1);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static ObjectiveLink Parse(string chatLink) => Parse(chatLink.AsSpan());

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static ObjectiveLink Parse(ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.WvWObjective)
        {
            throw new ArgumentException("Expected a WvW objective chat link.", nameof(chatLink));
        }

        var objectiveId = buffer.ReadUInt24();
        buffer.Padding(1);

        var mapId = buffer.ReadUInt24();
        buffer.Padding(1);

        return new ObjectiveLink
        {
            ObjectiveId = objectiveId,
            MapId = mapId
        };
    }
}
