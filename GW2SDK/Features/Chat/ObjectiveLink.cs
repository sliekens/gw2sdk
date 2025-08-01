﻿namespace GuildWars2.Chat;

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
        LinkBuffer buffer = new(stackalloc byte[9]);
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
    public static ObjectiveLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static ObjectiveLink Parse(in ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.WvWObjective)
        {
            ThrowHelper.ThrowBadArgument("Expected a WvW objective chat link.", nameof(chatLink));
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
