namespace GuildWars2.Chat;

/// <summary>Represents a coin chat link.</summary>
[PublicAPI]
public sealed record CoinLink : Link
{
    /// <summary>The amount of coins.</summary>
    public required Coin Coins { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Coin);
        buffer.WriteInt32(Coins.Amount);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static CoinLink Parse(string chatLink) => Parse(chatLink.AsSpan());

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static CoinLink Parse(ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Coin)
        {
            throw new ArgumentException("Expected a coin chat link.", nameof(chatLink));
        }

        var coins = buffer.ReadInt32();
        return new CoinLink { Coins = coins };
    }
}
