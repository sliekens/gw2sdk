namespace GuildWars2.Chat;

/// <summary>Represents a skin chat link.</summary>
[PublicAPI]
public sealed record SkinLink : Link
{
    /// <summary>The skin ID.</summary>
    public required int SkinId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Skin);
        buffer.WriteInt32(SkinId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static SkinLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static SkinLink Parse(in ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Skin)
        {
            ThrowHelper.ThrowBadArgument("Expected a skin chat link.", nameof(chatLink));
        }

        var skinId = buffer.ReadInt32();
        return new SkinLink { SkinId = skinId };
    }
}
