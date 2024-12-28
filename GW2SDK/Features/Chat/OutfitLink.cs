namespace GuildWars2.Chat;

/// <summary>Represents an outfit chat link.</summary>
[PublicAPI]
public sealed record OutfitLink : Link
{
    /// <summary>The outfit ID.</summary>
    public required int OutfitId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Outfit);
        buffer.WriteInt32(OutfitId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static OutfitLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static OutfitLink Parse(ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Outfit)
        {
            ThrowHelper.ThrowBadArgument("Expected an outfit chat link.", nameof(chatLink));
        }

        var outfitId = buffer.ReadInt32();
        return new OutfitLink { OutfitId = outfitId };
    }
}
