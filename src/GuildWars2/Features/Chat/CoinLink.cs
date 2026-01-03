namespace GuildWars2.Chat;

/// <summary>Represents a coin chat link.</summary>
public sealed record CoinLink : Link
{
    /// <summary>The amount of coins.</summary>
    public required Coin Coins { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        LinkBuffer buffer = new(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Coin);
        buffer.WriteInt32(Coins.Amount);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static CoinLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static CoinLink Parse(in ReadOnlySpan<char> chatLink)
    {
        Span<byte> bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Coin)
        {
            ThrowHelper.ThrowBadArgument("Expected a coin chat link.", nameof(chatLink));
        }

        int coins = buffer.ReadInt32();
        return new CoinLink { Coins = coins };
    }
}
