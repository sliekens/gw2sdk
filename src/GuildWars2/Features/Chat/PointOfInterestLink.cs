namespace GuildWars2.Chat;

/// <summary>Represents a point of interest chat link.</summary>
public sealed record PointOfInterestLink : Link
{
    /// <summary>The point of interest ID.</summary>
    public required int PointOfInterestId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        LinkBuffer buffer = new(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.PointOfInterest);
        buffer.WriteInt32(PointOfInterestId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static PointOfInterestLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static PointOfInterestLink Parse(in ReadOnlySpan<char> chatLink)
    {
        Span<byte> bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.PointOfInterest)
        {
            ThrowHelper.ThrowBadArgument(
                "Expected a point of interest chat link.",
                nameof(chatLink)
            );
        }

        int pointOfInterestId = buffer.ReadInt32();
        return new PointOfInterestLink { PointOfInterestId = pointOfInterestId };
    }
}
