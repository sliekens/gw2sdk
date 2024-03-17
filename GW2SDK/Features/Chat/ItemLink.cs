namespace GuildWars2.Chat;

/// <summary>Represents an item (stack) chat link.</summary>
[PublicAPI]
public sealed record ItemLink : Link
{
    /// <summary>The item ID.</summary>
    public required int ItemId { get; init; }

    /// <summary>The number of items in the stack.</summary>
    public int Count { get; init; } = 1;

    /// <summary>The skin ID of the item if the item is transmuted.</summary>
    /// <remarks>Only valid for weapons, armor and back items.</remarks>
    public int? SkinId { get; init; }

    /// <summary>The item ID of the upgrade component if the item is upgraded.</summary>
    /// <remarks>Only valid for weapons, armor, back items and trinkets.</remarks>
    public int? SuffixItemId { get; init; }

    /// <summary>The item ID of the second upgrade component if the item is upgraded.</summary>
    /// <remarks>Only valid for two-handed weapons.</remarks>
    public int? SecondarySuffixItemId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[18]);
        buffer.WriteUInt8(LinkHeader.Item);
        buffer.WriteUInt8(
            Count switch
            {
                < byte.MinValue => byte.MinValue,
                > byte.MaxValue => byte.MaxValue,
                _ => (byte)Count
            }
        );

        buffer.WriteUInt24(ItemId);

        var flagsAddress = buffer.Skip();
        if (SkinId.HasValue)
        {
            buffer.Buffer[flagsAddress] |= 0b1000_0000;
            buffer.WriteUInt24(SkinId.Value);
            buffer.WriteUInt8(default); // Unused bits
        }

        if (SuffixItemId.HasValue)
        {
            buffer.Buffer[flagsAddress] |= 0b0100_0000;
            buffer.WriteUInt24(SuffixItemId.Value);
            buffer.WriteUInt8(default); // Unused bits
        }

        if (SecondarySuffixItemId.HasValue)
        {
            buffer.Buffer[flagsAddress] |= 0b0010_0000;
            buffer.WriteUInt24(SecondarySuffixItemId.Value);
            buffer.WriteUInt8(default); // Unused bits
        }

        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static ItemLink Parse(string chatLink) => Parse(chatLink.AsSpan());

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static ItemLink Parse(ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Item)
        {
            throw new ArgumentException("Expected an item chat link.", nameof(chatLink));
        }

        var count = buffer.ReadUInt8();

        var itemId = buffer.ReadUInt24();

        var flags = buffer.ReadUInt8();

        int? skinId = default;
        if ((flags & 0b1000_0000) != 0)
        {
            skinId = buffer.ReadUInt24();
            buffer.Skip();
        }

        int? suffixItemId = default;
        if ((flags & 0b0100_0000) != 0)
        {
            suffixItemId = buffer.ReadUInt24();
            buffer.Skip();
        }

        int? secondarySuffixItemId = default;
        if ((flags & 0b0010_0000) != 0)
        {
            secondarySuffixItemId = buffer.ReadUInt24();
            buffer.Skip();
        }

        return new ItemLink
        {
            Count = count,
            ItemId = itemId,
            SkinId = skinId,
            SuffixItemId = suffixItemId,
            SecondarySuffixItemId = secondarySuffixItemId
        };
    }
}
